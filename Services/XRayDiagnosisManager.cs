using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Repositories.Contracts;
using Services.Contracts;
using Services.Utilities;
using System.Security.Claims;
using System.Text.Json;

namespace Services;

public class XRayDiagnosisManager : IXRayDiagnosisService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IImageStorageService _imageStorage;
    private readonly IPythonXrayClient _pythonClient;
    private readonly IGptClientService _gptClient;

    public XRayDiagnosisManager(IRepositoryManager repositoryManager, IImageStorageService imageStorage, IPythonXrayClient pythonClient, IGptClientService gptClient)
    {
        _repositoryManager = repositoryManager;
        _imageStorage = imageStorage;
        _pythonClient = pythonClient;
        _gptClient = gptClient;
    }

    public async Task<XRayResponseDto> PerformDiagnosisAsync(IFormFile imageFile, int userId)
    {
        // 1. Görüntüyü kaydet
        var filePath = await _imageStorage.SaveImageAsync(imageFile);

        // 2. Upload kaydını veritabanına ekle
        var xrayUpload = new XRayUpload
        {
            Id = Guid.NewGuid(),
            FileName = imageFile.FileName,
            FilePath = filePath,
            FileSize = imageFile.Length,
            UserId = userId
        };
        // Not: RepositoryManager'da doğrudan context'e erişim yerine
        // XRayUpload için bir repository oluşturmak daha doğru bir yaklaşımdır.
        _repositoryManager.Upload.CreateUpload(xrayUpload);
        
        // 3. Python servisini çağır
        var rawJsonResponse = await _pythonClient.AnalyzeXrayAsync(filePath);

        // 4. Analiz sonucunu veritabanına kaydet ve JSON'ı güvenli bir şekilde işle
        using var jsonDoc = JsonDocument.Parse(rawJsonResponse);
        var root = jsonDoc.RootElement;

        var confidence = root.TryGetProperty("confidence", out var confidenceElement) ? confidenceElement.GetDouble() : 0.0;
        var resultText = root.TryGetProperty("result", out var resultElement) ? resultElement.GetString() ?? "N/A" : "N/A";
        
        var analysisResult = new AnalysisResult
        {
            UploadId = xrayUpload.Id,
            Type = AnalysisType.XRay,
            RawJson = rawJsonResponse,
            Probability = confidence
        };
        _repositoryManager.AnalysisResult.Create(analysisResult);
        await _repositoryManager.SaveAsync();
        
        // 5. Bu analize bağlı bir sohbet oturumu oluştur
        var chatSession = new ChatSession
        {
            UserId = userId,
            AnalysisId = analysisResult.Id,
            CreatedAt = DateTime.UtcNow
        };
        _repositoryManager.ChatSession.Create(chatSession);
        await _repositoryManager.SaveAsync();

        // 6. GPT prompt'unu oluştur ve çağır
        var gptPayload = JsonSerializer.Serialize(new { result = resultText, confidence });
        var prompt = PromptFactory.BuildXrayPrompt(gptPayload);
        
        var (explanation, suggestions, tokens) = await _gptClient.GetResponseAsync(prompt);
        
        analysisResult.ResultSummary = explanation;
        
        // 7. AI log'unu kaydet
        _repositoryManager.AiPromptLog.Create(new AiPromptLog
        {
            AnalysisId = analysisResult.Id,
            PromptText = prompt,
            AiResponse = explanation,
            TokenCost = tokens
        });

        await _repositoryManager.SaveAsync();

        return new XRayResponseDto(
            analysisResult.Id, 
            resultText, 
            $"Confidence: {confidence:F2}%",
            explanation,
            suggestions);
    }
} 
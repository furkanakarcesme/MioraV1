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
    private readonly IQuickPromptService _quickPrompt;

    public XRayDiagnosisManager(IRepositoryManager repositoryManager, IImageStorageService imageStorage, IPythonXrayClient pythonClient, IGptClientService gptClient, IQuickPromptService quickPrompt)
    {
        _repositoryManager = repositoryManager;
        _imageStorage = imageStorage;
        _pythonClient = pythonClient;
        _gptClient = gptClient;
        _quickPrompt = quickPrompt;
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

        // 4. Analiz sonucunu veritabanına kaydet
        var analysisResult = new AnalysisResult
        {
            UploadId = xrayUpload.Id,
            Type = AnalysisType.XRay,
            RawJson = rawJsonResponse,
            Probability = JsonSerializer.Deserialize<JsonElement>(rawJsonResponse).GetProperty("confidence").GetDouble()
        };
        _repositoryManager.AnalysisResult.Create(analysisResult);
        await _repositoryManager.SaveAsync();
        
        // 5. GPT prompt'unu oluştur ve çağır
        var prompt = PromptFactory.BuildXrayPrompt(rawJsonResponse);
        var (explanation, tokens) = await _gptClient.GetResponseAsync(prompt);
        
        analysisResult.ResultSummary = explanation;
        
        // 6. AI log'unu kaydet
        _repositoryManager.AiPromptLog.Create(new AiPromptLog
        {
            AnalysisId = analysisResult.Id,
            PromptText = prompt,
            AiResponse = explanation,
            TokenCost = tokens
        });

        await _repositoryManager.SaveAsync();

        // 7. Yanıtı hazırla
        var suggestions = _quickPrompt.GetSuggestions(QuickPromptType.XRay).Suggestions;
        return new XRayResponseDto(analysisResult.Id, explanation, suggestions);
    }
} 
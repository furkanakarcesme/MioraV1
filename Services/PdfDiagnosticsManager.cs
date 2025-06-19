using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System.Text.Json;
using Services.Utilities;
using Microsoft.AspNetCore.Http;


namespace Services;

public class PdfDiagnosticsManager : IPdfDiagnosticsService
{
    private readonly IRepositoryManager _repositoryManager;
    // private readonly IPdfStorageService _pdfStorage; // Henüz eklenmedi
    private readonly IPythonPdfClient _pythonClient;
    private readonly IGptClientService _gptClient;
    private readonly IPdfStorageService _pdfStorage;

    // Laboratuvar referans aralıkları
    private static readonly Dictionary<string, (double min, double max)> LabReferenceRanges = new()
    {
        // Not: Bu değerler, sağlanan PDF ve genel tıbbi standartlara dayanmaktadır.
        // Bir tıp uzmanı tarafından gözden geçirilmelidir. Bazı değerler yaygın aralıklara göre varsayılan olarak eklenmiştir.
        { "CRP", (0, 5) },
        { "Calcium", (8.5, 10.5) },
        { "Creatinine", (0.6, 1.3) }, // Genel aralık (mg/dL), PDF'teki birim belirsiz.
        { "HDL", (35, double.MaxValue) }, // PDF'teki risk seviyelerinden basitleştirildi (>35 yüksek riskli değil).
        { "HbA1c_level", (4, 6) }, // PDF'ten (HbA1c).
        { "LDL", (0, 130) }, // Ortak hedef (<130 mg/dL), varsayılan.
        { "TSH", (0.38, 5.33) }, // PDF'ten.
        { "Vitamin_B12", (211, 911) }, // PDF'ten.
        { "WBC", (4.0, 11.0) }, // Genel aralık, varsayılan.
        { "Folat", (5.38, double.MaxValue) }, // PDF'ten (> 5.38).
        { "Ferritin", (22, 322) }, // PDF'ten.
        { "Kolesterol", (0, 200) } // PDF'ten (< 200).
    };

    public PdfDiagnosticsManager(IRepositoryManager repositoryManager, 
                                 IPythonPdfClient pythonClient, 
                                 IGptClientService gptClient, 
                                 IPdfStorageService pdfStorage)
    {
        _repositoryManager = repositoryManager;
        _pythonClient = pythonClient;
        _gptClient = gptClient;
        _pdfStorage = pdfStorage;
    }

    public async Task<PdfDiagnosticsResponseBase> PerformDiagnosticsAsync(IFormFile pdfFile, PdfAnalysisMode mode, int userId)
    {
        // 1. PDF'i kaydet
        var (filePath, pageCount) = await _pdfStorage.SavePdfAsync(pdfFile);
        var pdfUpload = new PdfUpload
        {
            Id = Guid.NewGuid(),
            FileName = pdfFile.FileName,
            FilePath = filePath,
            FileSize = pdfFile.Length,
            UserId = userId,
            Pages = pageCount
        };
        _repositoryManager.Upload.CreateUpload(pdfUpload);

        // 2. Python servisini çağır
        var rawJsonResponse = await _pythonClient.AnalyzePdfAsync(filePath, mode);

        // Geçici dosyayı sil (Storage servisleri wwwroot'a kaydettiği için buna gerek kalmadı)
        // File.Delete(tempFilePath); 

        // 3. Analiz sonucunu veritabanına kaydet
        var analysisResult = new AnalysisResult
        {
            UploadId = pdfUpload.Id,
            Type = mode == PdfAnalysisMode.Labs ? AnalysisType.Labs : AnalysisType.Diabetes,
            RawJson = rawJsonResponse,
            CreatedAt = DateTime.UtcNow
        };
        _repositoryManager.AnalysisResult.Create(analysisResult);
        await _repositoryManager.SaveAsync(); // Analiz ID'sinin oluşması için burada kaydediyoruz.

        // 4. Bu analize bağlı bir sohbet oturumu oluştur
        var chatSession = new ChatSession
        {
            Id = analysisResult.Id, // ChatSession ve AnalysisResult aynı ID'yi kullanır
            UserId = userId,
            AnalysisId = analysisResult.Id,
            CreatedAt = DateTime.UtcNow
        };
        _repositoryManager.ChatSession.Create(chatSession);
        await _repositoryManager.SaveAsync();

        // 5. Sonucu işle ve GPT'ye gönder
        if (mode == PdfAnalysisMode.Labs)
        {
            return await ProcessLabsResult(analysisResult);
        }
        else
        {
            return await ProcessDiabetesResult(analysisResult);
        }
    }

    private async Task<LabsPdfResponseDto> ProcessLabsResult(AnalysisResult analysisResult)
    {
        var pythonResponse = JsonSerializer.Deserialize<PythonLabResponseDto>(analysisResult.RawJson);
        var labValues = pythonResponse.LabValues;
        
        var observations = new List<LabObservationDto>();
        var enrichedLabValues = new Dictionary<string, object>();

        foreach (var entry in labValues)
        {
            var status = StatusEnum.Normal;
            if (LabReferenceRanges.TryGetValue(entry.Key, out var range))
            {
                if (entry.Value < range.min) status = StatusEnum.Low;
                if (entry.Value > range.max) status = StatusEnum.High;
            }
            
            observations.Add(new LabObservationDto(entry.Key, entry.Value));
            enrichedLabValues[entry.Key] = new { value = entry.Value, status = status.ToString() };
            
            _repositoryManager.LabObservation.Create(new LabObservation
            {
                AnalysisId = analysisResult.Id,
                Name = entry.Key,
                Value = entry.Value,
                Status = status
            });
        }

        var gptPayload = JsonSerializer.Serialize(enrichedLabValues);
        var prompt = PromptFactory.BuildLabsPrompt(gptPayload);
        
        var (explanation, suggestions, tokens) = await _gptClient.GetResponseAsync(prompt);

        analysisResult.ResultSummary = explanation;

        _repositoryManager.AiPromptLog.Create(new AiPromptLog
        {
            AnalysisId = analysisResult.Id,
            PromptText = prompt,
            AiResponse = explanation,
            TokenCost = tokens
        });
        
        await _repositoryManager.SaveAsync();
        
        return new LabsPdfResponseDto(analysisResult.Id, observations, explanation, suggestions);
    }
    
    private async Task<DiabetesPdfResponseDto> ProcessDiabetesResult(AnalysisResult analysisResult)
    {
        var predictionData = JsonSerializer.Deserialize<Dictionary<string, int>>(analysisResult.RawJson);
        var prediction = predictionData["diabetes_prediction"] == 1;

        var gptPayload = JsonSerializer.Serialize(new { prediction = prediction ? "possible-diabetic" : "non-diabetic" });
        var prompt = PromptFactory.BuildDiabetesPrompt(gptPayload);

        var (explanation, suggestions, tokens) = await _gptClient.GetResponseAsync(prompt);

        analysisResult.ResultSummary = explanation;

        _repositoryManager.AiPromptLog.Create(new AiPromptLog
        {
            AnalysisId = analysisResult.Id,
            PromptText = prompt,
            AiResponse = explanation,
            TokenCost = tokens
        });

        await _repositoryManager.SaveAsync();
        
        return new DiabetesPdfResponseDto(analysisResult.Id, prediction, explanation, suggestions);
    }
} 
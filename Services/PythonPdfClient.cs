using Microsoft.AspNetCore.Http;
using Services.Contracts;
using System.Net.Http.Headers;

namespace Services;

public class PythonPdfClient : IPythonPdfClient
{
    private readonly HttpClient _httpClient;

    public PythonPdfClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> AnalyzePdfAsync(string filePath, PdfAnalysisMode mode)
    {
        await using var fileStream = File.OpenRead(filePath);
        using var content = new MultipartFormDataContent();
        using var streamContent = new StreamContent(fileStream);
        
        streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        content.Add(streamContent, "file", Path.GetFileName(filePath));
        content.Add(new StringContent(mode.ToString().ToLowerInvariant()), "type");

        var response = await _httpClient.PostAsync("/analyze-pdf", content);
        
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadAsStringAsync();
    }
} 
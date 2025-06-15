using Services.Contracts;
using System.Net.Http.Headers;

namespace Services;

public class PythonXrayClient : IPythonXrayClient
{
    private readonly HttpClient _httpClient;

    public PythonXrayClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> AnalyzeXrayAsync(string imagePath)
    {
        await using var fileStream = File.OpenRead(imagePath);
        using var content = new MultipartFormDataContent();
        using var streamContent = new StreamContent(fileStream);

        var extension = Path.GetExtension(imagePath).ToLowerInvariant();
        var contentType = extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            _ => "application/octet-stream"
        };
        
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(streamContent, "file", Path.GetFileName(imagePath));

        var response = await _httpClient.PostAsync("/predict", content);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
} 
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Services.Contracts;

namespace Services;

public class GptClientService : IGptClientService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public GptClientService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<(string Response, List<string> Suggestions, int Tokens)> GetResponseAsync(string prompt)
    {
        var model = _configuration["GptSettings:Model"];
        
        var requestBody = new GptRequest
        {
            Model = model,
            Messages = new List<GptMessage>
            {
                new GptMessage { Role = "system", Content = "You are a helpful medical assistant. Analyze the following lab results and provide a summary in Turkish. Be concise and clear." },
                new GptMessage { Role = "user", Content = prompt }
            },
            Temperature = 0.5,
            MaxTokens = 1000
        };

        var jsonBody = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("chat/completions", content);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var gptResponse = JsonSerializer.Deserialize<GptResponse>(responseBody);

        var message = gptResponse?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response from AI.";
        var tokens = gptResponse?.Usage?.TotalTokens ?? 0;
        
        var parts = message.Split(new[] { "|||" }, StringSplitOptions.None);
        var mainAnswer = parts[0].Trim();
        var suggestions = new List<string>();

        if (parts.Length > 1)
        {
            suggestions = parts[1]
                .Trim()
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToList();
        }

        return (mainAnswer, suggestions, tokens);
    }
    
    // Internal DTOs for OpenAI API
    private class GptRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }
        
        [JsonPropertyName("messages")]
        public List<GptMessage> Messages { get; set; }
        
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }
        
        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; }
    }

    private class GptMessage
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    private class GptResponse
    {
        [JsonPropertyName("choices")]
        public List<GptChoice> Choices { get; set; }
        
        [JsonPropertyName("usage")]
        public GptUsage Usage { get; set; }
    }

    private class GptChoice
    {
        [JsonPropertyName("message")]
        public GptMessage Message { get; set; }
    }

    private class GptUsage
    {
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
} 
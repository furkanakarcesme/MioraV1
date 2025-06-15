namespace Services.Contracts;

public interface IGptClientService
{
    Task<(string Response, List<string> Suggestions, int Tokens)> GetResponseAsync(string prompt);
} 
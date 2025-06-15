namespace Services.Contracts;

public interface IGptClientService
{
    Task<(string Response, int Tokens)> GetResponseAsync(string prompt);
} 
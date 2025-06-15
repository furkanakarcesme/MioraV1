namespace Services.Contracts;

public interface IPythonXrayClient
{
    Task<string> AnalyzeXrayAsync(string imagePath);
} 
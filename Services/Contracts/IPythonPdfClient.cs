namespace Services.Contracts;

public enum PdfAnalysisMode { Labs, Diabetes }

public interface IPythonPdfClient
{
    Task<string> AnalyzePdfAsync(string filePath, PdfAnalysisMode mode);
} 
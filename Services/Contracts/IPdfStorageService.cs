using Microsoft.AspNetCore.Http;

namespace Services.Contracts;

public interface IPdfStorageService
{
    Task<(string FilePath, int Pages)> SavePdfAsync(IFormFile pdfFile);
} 
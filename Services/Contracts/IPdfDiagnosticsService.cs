using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Services.Contracts;

namespace Services.Contracts;

public interface IPdfDiagnosticsService
{
    Task<PdfDiagnosticsResponseBase> PerformDiagnosticsAsync(IFormFile pdfFile, PdfAnalysisMode mode, int userId);
} 
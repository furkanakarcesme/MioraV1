using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/pdf-diagnostics")]
public class PdfDiagnosticsController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public PdfDiagnosticsController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpPost("labs")]
    [RequestSizeLimit(30 * 1024 * 1024)] // 30 MB
    public async Task<IActionResult> UploadLabsPdf(IFormFile file)
    {
        return await HandlePdfUpload(file, PdfAnalysisMode.Labs);
    }
    
    [HttpPost("diabetes")]
    [RequestSizeLimit(30 * 1024 * 1024)] // 30 MB
    public async Task<IActionResult> UploadDiabetesPdf(IFormFile file)
    {
        return await HandlePdfUpload(file, PdfAnalysisMode.Diabetes);
    }

    private async Task<IActionResult> HandlePdfUpload(IFormFile file, PdfAnalysisMode mode)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("File is not provided or empty.");
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var result = await _serviceManager.PdfDiagnostics.PerformDiagnosticsAsync(file, mode, userId);
        return Ok(result);
    }
} 
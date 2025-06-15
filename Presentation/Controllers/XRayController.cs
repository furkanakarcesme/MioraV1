using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers;

//[Authorize]
[ApiController]
[Route("api/xray")]
public class XRayController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public XRayController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB limit
    public async Task<IActionResult> UploadXRay(IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("File is not provided or empty.");
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null || !int.TryParse(userIdClaim, out var userId))
        {
            // HACK: For testing purposes without authentication, default to user ID 1.
            // In a real scenario, this should return Unauthorized().
            userId = 1;
            // return Unauthorized();
        }
        
        var result = await _serviceManager.XRayDiagnosis.PerformDiagnosisAsync(file, userId);
        return Ok(result);
    }
} 
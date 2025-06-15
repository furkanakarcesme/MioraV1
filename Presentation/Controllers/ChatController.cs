using Entities.DataTransferObjects;
using Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers;

//[Authorize]
[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ChatController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet("suggestions/{analysisId:int}")]
    public async Task<IActionResult> GetSuggestions(int analysisId)
    {
        // Analizin tipini bulup doğru QuickPromptType'ı seçmemiz gerekiyor.
        // Bu bilgi AnalysisResult'ta var.
        var analysis = await _serviceManager.Repositories.AnalysisResult
            .FindByCondition(a => a.Id == analysisId, trackChanges: false)
            .FirstOrDefaultAsync();

        if (analysis is null)
        {
            return NotFound();
        }
        
        var promptType = analysis.Type switch
        {
            AnalysisType.Labs => QuickPromptType.Labs,
            AnalysisType.XRay => QuickPromptType.XRay,
            AnalysisType.Diabetes => QuickPromptType.Diabetes,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var suggestions = _serviceManager.QuickPrompt.GetSuggestions(promptType);
        return Ok(suggestions);
    }
    
    [HttpPost("messages")]
    public async Task<IActionResult> PostMessage([FromBody] ChatMessageRequest request)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        // var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // if (userIdClaim is null || !int.TryParse(userIdClaim, out var userId))
        // {
        //     return Unauthorized();
        // }
        
        var userId = 1; //TODO: Will be changed after authentication.
        
        var result = await _serviceManager.Chat.ProcessUserMessageAsync(request, userId);
        return Ok(result);
    }
} 
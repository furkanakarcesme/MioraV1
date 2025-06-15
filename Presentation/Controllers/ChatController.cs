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
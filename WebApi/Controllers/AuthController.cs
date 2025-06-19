using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using WebApi.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
    {
        var tokenDto = await _authService.RegisterAsync(userForRegistrationDto);
        return Ok(tokenDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthenticationDto)
    {
        var tokenDto = await _authService.LoginAsync(userForAuthenticationDto);
        return Ok(tokenDto);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var newTokens = await _authService.RefreshTokenAsync(tokenDto);
        return Ok(newTokens);
    }
    
    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var userDto = await _authService.GetUserProfileAsync(userId);
        if (userDto is null)
        {
            return NotFound("User not found.");
        }

        return Ok(userDto);
    }
} 
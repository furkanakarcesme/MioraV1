using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Entities.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto dto)
        {
            var result = await _authService.RegisterUserAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var newTokens = await _authService.RefreshTokenAsync(tokenDto);
            if (newTokens == null)
                return BadRequest("Invalid refresh token.");
            return Ok(newTokens);
        }
    }
} 
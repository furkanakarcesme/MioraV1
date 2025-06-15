using Entities.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto dto);
    Task<TokenDto?> LoginAsync(UserForAuthenticationDto dto);
    Task<TokenDto?> RefreshTokenAsync(TokenDto tokenDto);
} 
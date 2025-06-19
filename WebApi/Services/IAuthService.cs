using Entities.DataTransferObjects;

namespace WebApi.Services;

public interface IAuthService
{
    Task<TokenDto> RegisterAsync(UserForRegistrationDto userForRegistrationDto);
    Task<TokenDto> LoginAsync(UserForAuthenticationDto userForAuthenticationDto);
    Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto);
    Task<UserDto?> GetUserProfileAsync(string userId);
} 
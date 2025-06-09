using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Utilities;

namespace WebApi.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded && dto.Roles != null)
            {
                await _userManager.AddToRolesAsync(user, dto.Roles);
            }
            return result;
        }

        public async Task<TokenDto?> LoginAsync(UserForAuthenticationDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = GenerateJwtToken(user, roles);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _userManager.UpdateAsync(user);

            return new TokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        public async Task<TokenDto?> RefreshTokenAsync(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var userEmail = principal.Identity?.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null || user.RefreshToken != token || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = GenerateJwtToken(user, roles);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new TokenDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        private string GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name ?? "")
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
} 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Application.Common.Dto;
using RedArbor.Domain.Constants;

namespace RedArbor.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IConfiguration _configuration;


    private IConfigurationSection _jwtSettings
    {
        get => _configuration.GetSection("Jwt");
    }

    private readonly string _jwtKey;
    private readonly double _tokenExpireMinutes;
    private readonly double _refreshTokenExpireMinutes;

    private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(IConfiguration configuration, IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _configuration = configuration;
        _jwtKey = _jwtSettings["Key"]
            ?? throw new InvalidOperationException("JWT Key is not configured.");
        _tokenExpireMinutes = double.Parse(_jwtSettings["ExpireMinutes"]
            ?? throw new InvalidOperationException("JWT ExpireMinutes is not configured."));
        _refreshTokenExpireMinutes = double.Parse(_jwtSettings["RefreshTokenExpireMinutes"]
            ?? throw new InvalidOperationException("JWT RefreshTokenExpireMinutes is not configured."));

        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public string GetUserId()
    {
        throw new NotImplementedException();
    }

    public TokenDto GenerateToken(LoginDto loginDto)
    {
        if (loginDto.Username == "admin" && loginDto.Password == "password")
        {
            return new TokenDto
            {
                Token = GenerateJwtToken(loginDto.Username, Roles.Administrator, _tokenExpireMinutes),
                ExpiresIn = _tokenExpireMinutes,
                RefreshToken = GenerateJwtToken(loginDto.Username, Roles.Administrator, _refreshTokenExpireMinutes),
                RefreshTokenExpiresIn = _refreshTokenExpireMinutes
            };
        }
        else if (loginDto.Username == "user1" && loginDto.Password == "password")
        {
            return new TokenDto
            {
                Token = GenerateJwtToken(loginDto.Username, Roles.User, _tokenExpireMinutes),
                ExpiresIn = _tokenExpireMinutes,
                RefreshToken = GenerateJwtToken(loginDto.Username, Roles.User, _refreshTokenExpireMinutes),
                RefreshTokenExpiresIn = _refreshTokenExpireMinutes
            };
        }

        throw new UnauthorizedAccessException("Invalid credentials");
    }

    private string GenerateJwtToken(string username, string role, double expireMinutes)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

        var claims = new[]
        {            
            // new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            // new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Role, "Otro rol")
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings["Issuer"],
            audience: _jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }





    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = new AppUser
        {
            Id = userId
        };

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }
}
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

    private readonly IUserStore<AppUser> _userStore;
    // private readonly IRoleStore<AppRole> _roleStore;
    // private readonly IUserRoleStore<AppUser> _userRoleStore;


    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    public IdentityService(IConfiguration configuration,
        IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,

        IUserStore<AppUser> userStore,
        // IRoleStore<AppRole> roleStore,
        // IUserRoleStore<AppUser> userRoleStore,

        RoleManager<AppRole> roleManager,
        UserManager<AppUser> userManager

        )
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

        _userStore = userStore;
        // _roleStore = roleStore;
        // _userRoleStore = userRoleStore;

        _roleManager = roleManager;
        _userManager = userManager;
    }

    public string GetUserId()
    {
        throw new NotImplementedException();
    }

    public async Task<TokenDto> GenerateToken(LoginDto loginDto)
    {
        var user = await GetUser(loginDto.Username, loginDto.Password);
        return new TokenDto
        {
            Token = GenerateJwtToken(user, _tokenExpireMinutes),
            ExpiresIn = _tokenExpireMinutes,
            RefreshToken = GenerateJwtToken(user, _refreshTokenExpireMinutes),
            RefreshTokenExpiresIn = _refreshTokenExpireMinutes
        };

    }

    private string GenerateJwtToken(AppUser user, double expireMinutes)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

        List<Claim> claims = [
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!)
        ];

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name!));
        }

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
        var user = await _userStore.FindByIdAsync(userId, CancellationToken.None);


        if (user == null)
        {
            return false;
        }

        // await InitData(user);


        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    private async Task<AppUser> GetUser(string userName, string password)
    {
        AppUser? appUser = await _userStore.FindByNameAsync(userName.ToUpperInvariant(),
        CancellationToken.None);

        if (appUser == null || appUser.PasswordHash != password)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        return appUser;
    }



}




using System.Security.Claims;
using RedArbor.Application.Common.Interfaces;

namespace RedArbor.WebApi.Services;


public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string UserId
    {
        get
        {
            // var userId = GetClaim(JwtRegisteredClaimNames.Jti);
            var userId = GetClaim(ClaimTypes.NameIdentifier);
            return userId ?? string.Empty;
        }
    }


    public string UserName
    {
        get
        {
            // var userName = GetClaim(JwtRegisteredClaimNames.Sub);
            var userName = GetClaim(ClaimTypes.Name);
            return userName ?? string.Empty;
        }
    }

    public IEnumerable<string> Roles
    {
        get
        {
            var roles = _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToArray() ?? [];
            return roles;
        }
    }

    private string? GetClaim(string claimType)
    {
        string? value = _httpContextAccessor.HttpContext?.User?.FindFirstValue(claimType);
        return value;
    }
}
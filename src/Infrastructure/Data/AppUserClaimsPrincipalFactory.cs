namespace RedArbor.Infrastructure.Data;

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Infrastructure.Identity;


/// <summary>
/// Custom ClaimsPrincipalFactory to include user roles from IUser service
/// </summary>
/// <param name="userManager">User manager</param>
/// <param name="roleManager">Roles manager</param>
/// <param name="optionsAccessor">Options accessor</param>
/// <param name="authUser">Authentication user</param>
public class AppUserClaimsPrincipalFactory(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IOptions<IdentityOptions> optionsAccessor,
    IUser authUser) 
    : UserClaimsPrincipalFactory<AppUser, AppRole>(userManager, roleManager, optionsAccessor)
{
    private readonly IUser _authUser = authUser;

    /// <summary>
    /// Generates claims for the user, including roles
    /// </summary>
    /// <param name="user">App user</param>
    /// <returns> ClaimsIdentity with roles added </returns>
    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        foreach (var role in _authUser.Roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return identity;
    }
}
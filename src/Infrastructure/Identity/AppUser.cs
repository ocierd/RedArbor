using Microsoft.AspNetCore.Identity;

namespace RedArbor.Infrastructure.Identity;

/// <summary>
/// Application user class
/// </summary>
public class AppUser : IdentityUser<string>
{
    /// <summary>
    /// Roles assigned to the user
    /// </summary>
    public IEnumerable<AppRole> Roles { get; set; } = [];
}
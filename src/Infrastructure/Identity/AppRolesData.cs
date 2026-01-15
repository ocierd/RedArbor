using RedArbor.Domain.Constants;

namespace RedArbor.Infrastructure.Identity;

/// <summary>
/// Static class containing predefined application roles data
/// </summary>
public class AppRolesData
{
    /// <summary>
    /// Administrator role
    /// </summary>
    public static readonly AppRole AdministratorRole = new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = Roles.Administrator,
        NormalizedName = Roles.Administrator.ToUpperInvariant()
    };

    /// <summary>
    /// User role
    /// </summary>
    public static readonly AppRole UserRole = new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = Roles.User,
        NormalizedName = Roles.User.ToUpperInvariant()
    };

    /// <summary>
    /// Inventory Manager role
    /// </summary>
    public static readonly AppRole InventoryManagerRole = new()
    {
        Id = Guid.NewGuid().ToString(),
        Name = Roles.InventoryManager,
        NormalizedName = Roles.InventoryManager.ToUpperInvariant()
    };

    /// <summary>
    /// Predefined application roles
    /// </summary>
    public static readonly List<AppRole> RolesData = [
        AdministratorRole,
        UserRole,
        InventoryManagerRole
    ];
}

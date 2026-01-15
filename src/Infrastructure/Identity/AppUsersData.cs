namespace RedArbor.Infrastructure.Identity;

/// <summary>
/// Static class containing predefined application users data
/// </summary>
public class AppUsersData
{

    /// <summary>
    /// Predefined application users
    /// </summary>
    public static readonly List<AppUser> appUsers =
    [
        new AppUser
        {
            Id = "1",
            UserName = "admin",
            Email = "admin@example.com",
            EmailConfirmed = true,
            AccessFailedCount = 0,
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            NormalizedUserName = "ADMIN",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            SecurityStamp = string.Empty,
            PasswordHash = "Password123!",
            Roles = [AppRolesData.AdministratorRole, AppRolesData.InventoryManagerRole]
        },
        new AppUser
        {
            Id = "2",
            UserName = "user1",
            Email = "user1@example.com",
            EmailConfirmed = true,
            AccessFailedCount = 0,
            NormalizedEmail = "USER1@EXAMPLE.COM",
            NormalizedUserName = "USER1",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            SecurityStamp = string.Empty,
            PasswordHash = "Password123!",
            Roles = [AppRolesData.UserRole]
        },
        new AppUser
        {
            Id = "3",
            UserName = "inventoryManager",
            Email = "inventoryManager@example.com",
            EmailConfirmed = true,
            AccessFailedCount = 0,
            NormalizedEmail = "INVENTORYMANAGER@EXAMPLE.COM",
            NormalizedUserName = "INVENTORYMANAGER",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnabled = false,
            SecurityStamp = string.Empty,
            PasswordHash = "Password123!",
            Roles = [AppRolesData.InventoryManagerRole]
        }
    ];
}

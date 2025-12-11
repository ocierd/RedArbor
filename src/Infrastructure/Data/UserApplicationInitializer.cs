using Microsoft.AspNetCore.Identity;
using RedArbor.Domain.Constants;
using RedArbor.Infrastructure.Identity;

namespace RedArbor.Infrastructure.Data;



public class UserApplicationInitializer(UserManager<AppUser> userManager
, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
{
    private readonly UserManager<AppUser> _userManager = userManager;

    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    private readonly ApplicationDbContext _context = context;

    public async Task InitializeAsync()
    {

        if (!_roleManager.Roles.All(r => r.Name != Roles.Administrator && r.Name != Roles.User))
        {
            var adminRole = new IdentityRole(Roles.Administrator);
            var userRole = new IdentityRole(Roles.User);
            await _roleManager.CreateAsync(adminRole);
            await _roleManager.CreateAsync(userRole);
        }

        var adminUser = new AppUser
        {
            UserName = "admin",
            Id = Guid.NewGuid().ToString(),
            Email = "admin@example.com",
            PasswordHash = "Administrator@123"
        };
        // Seed initial data if necessary
        if (!_userManager.Users.All(u => u.UserName != adminUser.UserName))
        {
            await _userManager.CreateAsync(adminUser);
        }

        var simpleUser = new AppUser
        {
            UserName = "user1",
            Id = Guid.NewGuid().ToString(),
            Email = "user1@example.com",
            PasswordHash = "User@123"
        };
        if (!_userManager.Users.All(u => u.UserName != simpleUser.UserName))
        {
            await _userManager.CreateAsync(simpleUser);
        }
    }
}
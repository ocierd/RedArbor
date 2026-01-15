using Microsoft.AspNetCore.Identity;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Infrastructure.Identity;

namespace RedArbor.Infrastructure.Data;

/// <summary>
/// In-memory user store for AppUser
/// </summary>
public class AppUserStore : IUserStore<AppUser>
{

    private static readonly List<AppUser> _users = AppUsersData.appUsers;

    public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        IdentityResult result = IdentityResult.Success;

        return result;
    }

    public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<AppUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken); // Simulate async operation

        return _users.FirstOrDefault(u => u.Id == userId);
    }

    public async Task<AppUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await Task.FromResult(_users.FirstOrDefault(u => u.UserName?.ToUpperInvariant() == normalizedUserName));
    }

    public Task<string?> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName?.ToUpperInvariant());
    }

    public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id);
    }

    public Task<string?> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetNormalizedUserNameAsync(AppUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetUserNameAsync(AppUser user, string? userName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
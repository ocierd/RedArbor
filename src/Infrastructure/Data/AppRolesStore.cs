using Microsoft.AspNetCore.Identity;
using RedArbor.Infrastructure.Identity;


namespace RedArbor.Infrastructure.Data;

/// <summary>
/// In-memory roles store for AppRole
/// </summary>
public class AppRolesStore : IRoleStore<AppRole>
{
    private static readonly List<AppRole> _roles = [
        
    ];

    public Task<IdentityResult> CreateAsync(AppRole role, CancellationToken cancellationToken)
    {
        _roles.Add(role);
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(AppRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public Task<AppRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_roles.FirstOrDefault(r => r.Id == roleId));
    }

    public Task<AppRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return Task.FromResult(_roles.FirstOrDefault(r => r.NormalizedName == normalizedRoleName));
    }

    public Task<string?> GetNormalizedRoleNameAsync(AppRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(AppRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetRoleNameAsync(AppRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(AppRole role, string? normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(AppRole role, string? roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(AppRole role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
namespace RedArbor.Application.Common.Interfaces;

/// <summary>
/// Identity service interface
/// </summary>
public interface IIdentityService
{
    string GetUserId();

    Task<TokenDto> GenerateToken(LoginDto loginDto);

    Task<bool> AuthorizeAsync(string userId, string policyName);
}
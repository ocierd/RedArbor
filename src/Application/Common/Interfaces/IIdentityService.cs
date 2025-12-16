namespace RedArbor.Application.Common.Interfaces;

public interface IIdentityService
{
    string GetUserId();

    TokenDto GenerateToken(LoginDto loginDto);

    Task<bool> AuthorizeAsync(string userId, string policyName);
}
namespace RedArbor.Application.Auth.Queries.Handlers;

/// <summary>
/// Handler for LoginQuery
/// </summary>
/// <param name="identityService"> The identity service used for authentication </param>
public class LoginHandler(IIdentityService identityService) : IRequestHandler<LoginQuery, TokenDto>
{
    private readonly IIdentityService _identityService = identityService;

    public async Task<TokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var token = _identityService.GenerateToken(new LoginDto
        {
            Username = request.Username,
            Password = request.Password
        });
        return token;
    }
}
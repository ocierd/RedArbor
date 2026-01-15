using Microsoft.AspNetCore.Authorization;
using RedArbor.Application.Auth.Queries;

namespace RedArbor.WebApi.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
/// <param name="mediator"></param>
[ApiController]
[Route("api/[controller]")]
public class AuthController (IMediator mediator): ApiControllerBase
{
    private readonly IMediator mediator = mediator;

    /// <summary>
    /// Authenticates a user and returns a JWT token
    /// </summary>
    /// <param name="loginquery">Request containing login credentials</param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AllowAnonymous]
    [ProducesResponseType<TokenDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<TokenDto> Authenticate([FromBody] LoginQuery loginquery)
    {
        var token = await mediator.Send(loginquery);
        return token;
    }


}
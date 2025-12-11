using Microsoft.AspNetCore.Authorization;
using RedArbor.Application.Auth.Queries;

namespace RedArbor.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController (IMediator mediator): ApiControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpPost("[action]")]
    [AllowAnonymous]
    [ProducesResponseType<TokenDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<TokenDto> Authenticate([FromBody] LoginQuery command)
    {
        var token = await mediator.Send(command);
        return token;
    }


}
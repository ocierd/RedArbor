using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedArbor.Application.WeatherForecasts.Query;

namespace RedArbor.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var res = await _mediator.Send(new WeatherForecastGetAllQuery());
        return res;
    }
}

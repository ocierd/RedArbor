namespace RedArbor.Application.WeatherForecasts.Query.Handlers;

/// <summary>
/// Handles the retrieval of all weather forecasts.
/// </summary>
public class WeatherforecastGetAllHandler : IRequestHandler<WeatherForecastGetAllQuery, IEnumerable<WeatherForecast>>
{
    /// <summary>
    /// Possible weather summaries.
    /// </summary>
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];


    /// <summary>
    /// Handles the WeatherForecastGetAllQuery to return a list of weather forecasts.
    /// </summary>
    /// <param name="request"> The query request containing parameters for retrieving weather forecasts.</param>
    /// <param name="cancellationToken"> The cancellation token that can cancel the operation.</param>
    /// <returns>Operation result containing a collection of weather forecasts.</returns>
    public async Task<IEnumerable<WeatherForecast>> Handle(WeatherForecastGetAllQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(0,cancellationToken);

        return [..Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })];
    }
}
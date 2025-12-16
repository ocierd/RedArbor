namespace RedArbor.Application.WeatherForecasts.Query;

using MediatR;
using RedArbor.Application.Common.Dto;
using System.Collections.Generic;

/// <summary>
/// Query to get all weather forecasts.
/// </summary>
public record WeatherForecastGetAllQuery : IRequest<IEnumerable<WeatherForecast>>;
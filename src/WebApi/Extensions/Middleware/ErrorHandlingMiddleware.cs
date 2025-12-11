using System.Net.Mime;
using System.Text.Json;
using RedArbor.Application.Common.Exceptions;

namespace RedArbor.WebApi.Extensions.Middleware;

/// <summary>
/// Middleware for handling custom exceptions and returning appropiate HTTP responses
/// </summary>
/// <param name="next">Next middleware in the pipeline</param>
/// <param name="logger">Logger for logging errors</param>
public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation error: {Errors}", ex.Errors);
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await JsonSerializer.SerializeAsync(context.Response.Body, new { Message = "Validation Errors", ex.Errors });
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning("Not found error: {Message}", ex.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await JsonSerializer.SerializeAsync(context.Response.Body, new { ex.Message });
        }
        catch (UnauthorizedException ex)
        {
            _logger.LogWarning("Unauthorized error: {Message}", ex.Message);
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await JsonSerializer.SerializeAsync(context.Response.Body, new { Message = "Unauthorized access" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await JsonSerializer.SerializeAsync(context.Response.Body, new { Message = "An unexpected error occurred." });
        }


    }
}
using RedArbor.WebApi.Extensions.Middleware;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods to add middlewares to the DI container
/// </summary>
public static class MiddlewareDependencyInjection
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }
}

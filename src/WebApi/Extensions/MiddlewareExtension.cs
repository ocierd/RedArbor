using RedArbor.WebApi.Extensions.Middleware;

namespace Microsoft.Extensions.DependencyInjection;


public static class MiddlewareDependencyInjection
{
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        return app;
    }
}

using Redarbor.Application.Common.Behaviours;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Dependency injection for Application layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Application services to the DI container
    /// </summary>
    /// <param name="services">Service collection to add the application services to</param>
    /// <returns>The same service collection with application services added</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));

        });

        return services;
    }
}

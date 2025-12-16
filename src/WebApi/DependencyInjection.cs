using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Infrastructure.Data;
using RedArbor.WebApi.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers(opts =>
        {
            var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
            opts.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IUser, CurrentUser>();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        services.AddOpenApi();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static async Task InitializeData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<UserApplicationInitializer>();
        await initialiser.InitializeAsync();
    }



}
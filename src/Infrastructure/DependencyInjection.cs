// namespace RedArbor.Infrastructure;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Application.Common.Interfaces.Repository;
using RedArbor.Domain.Constants;
using RedArbor.Infrastructure.Data;
using RedArbor.Infrastructure.Data.Repository;
using RedArbor.Infrastructure.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services
    , IConfiguration configuration)
    {
        string connectionName = "Products";
        string connectionString = configuration.GetConnectionString(connectionName)
            ?? throw new ArgumentException("Connection string not found.", nameof(connectionName));

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opts =>
        {
            opts.UseSqlServer(connectionString);
        });

        services.AddSingleton(opts => configuration);

        // services.AddScoped<UserApplicationInitializer>();
        services.AddDefaultIdentity<AppUser>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

        // Register DapperContext and Repositories
        services.AddScoped<IDapperContext, DapperContext>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddAuthorizationBuilder()
        .AddPolicy(Policies.CanGetAllProducts, policy => policy.RequireRole(Roles.Administrator));

        return services;
    }

    public static IServiceCollection AddAuthenticationJwt(this IServiceCollection services, IConfiguration configuration)
    {
        // Authentication and Authorization configuration can be added here in the future
        // Read JWT settings from configuration
        var jwtSettings = configuration.GetSection("Jwt");
        string keyValue = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");
        var key = Encoding.UTF8.GetBytes(keyValue);

        // Add Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero // No extra time beyond expiration
            };
        });

        services.AddAuthorization();
        return services;
    }

}

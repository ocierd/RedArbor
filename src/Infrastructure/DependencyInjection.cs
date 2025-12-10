// namespace RedArbor.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Application.Common.Interfaces.Repository;
using RedArbor.Infrastructure.Data;
using RedArbor.Infrastructure.Data.Repository;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services
    , IConfiguration configuration)
    {
        string connectionName = "Products";
        string connectionString = configuration.GetConnectionString(connectionName)
            ?? throw new ArgumentNullException("Connection string not found.",nameof(connectionName));

        services.AddDbContext<IApplicationDbContext,ApplicationDbContext>(opts =>
        {
            opts.UseSqlServer(connectionString);
        });

        
        services.AddSingleton(opts=> configuration);

        // Register DapperContext and Repositories
        services.AddScoped<IDapperContext, DapperContext>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }

}

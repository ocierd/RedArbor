// namespace RedArbor.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Infrastructure.Data;

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

        return services;
    }

}

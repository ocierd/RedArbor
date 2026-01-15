using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Domain.Entities;
using RedArbor.Infrastructure.Identity;

namespace RedArbor.Infrastructure.Data;

/// <summary>
/// Implementation of ApplicationDbContext
/// </summary>
/// <param name="options">Options for DbContext configuration</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: IdentityDbContext<AppUser,AppRole,string>(options), IApplicationDbContext
{
    /// <summary>
    /// DbSet for Products
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// DbSet for Categories
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>
    /// DbSet for Inventories
    /// </summary>
    public DbSet<Inventory> Inventories => Set<Inventory>();

    /// <summary>
    /// DbSet for Transactions
    /// </summary>
    public DbSet<Transaction> Transactions => Set<Transaction>();

    /// <summary>
    /// Configures the model
    /// </summary>
    /// <param name="builder">ModelBuilder instance for configuring the model</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Apply all configurations from the current assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

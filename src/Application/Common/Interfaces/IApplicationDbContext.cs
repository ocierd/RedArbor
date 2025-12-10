using Microsoft.EntityFrameworkCore;
using RedArbor.Domain.Entities;

namespace RedArbor.Application.Common.Interfaces;

/// <summary>
/// Interface for ApplicationDbContext
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// DbSet for Products
    /// </summary>
    DbSet<Product> Products { get; }

    /// <summary>
    /// DbSet for Categories
    /// </summary>
    DbSet<Category> Categories { get; }

    /// <summary>
    /// DbSet for Inventories
    /// </summary>
    DbSet<Inventory> Inventories { get; }

    /// <summary>
    /// DbSet for Transactions
    /// </summary>
    DbSet<Transaction> Transactions { get; }

}

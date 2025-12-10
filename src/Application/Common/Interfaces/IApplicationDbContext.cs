using Microsoft.EntityFrameworkCore;
using RedArbor.Domain.Entities;

namespace RedArbor.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    DbSet<Category> Categories { get; }

    DbSet<Inventory> Inventories { get; }

    DbSet<Transaction> Transactions { get; }

}

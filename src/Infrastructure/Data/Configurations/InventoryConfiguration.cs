
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedArbor.Domain.Entities;

namespace RedArbor.Infrastructure.Data.Configurations;

/// <summary>
/// Configuration for Inventory entity
/// </summary>
public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventory");

        builder.HasKey(e => e.InventoryId).HasName("inventory_pk");

        builder.Property(e => e.InventoryId)
            .HasColumnName("inventory_id")
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(e => e.ProductId)
            .HasColumnName("product_id")
            .IsRequired();




    }
}
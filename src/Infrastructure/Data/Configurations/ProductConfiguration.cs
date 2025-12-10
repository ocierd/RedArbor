using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedArbor.Domain.Entities;

namespace RedArbor.Infrastructure.Data.Configurations;

/// <summary>
/// Configuration for Product entity
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.ProductId).HasName("product_pk");
        
        builder.Property(p=>p.ProductId).HasColumnName("product_id");
        
        builder.Property(p => p.Name)
        .HasColumnName("name")
        .IsRequired().HasMaxLength(100);
        
        builder.Property(p => p.Description)
        .HasColumnName("description")
        .HasMaxLength(4096);

        builder.Property(p=>p.Price)
        .HasColumnName("price");

        builder.Property(p=>p.CreatedAt)
        .HasColumnName("created_at");

        builder.Property(p=>p.CategoryId)
        .HasColumnName("category_id");
    }
}

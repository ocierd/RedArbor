using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedArbor.Domain.Entities;

namespace RedArbor.Infrastructure.Data.Configurations;

/// <summary>
/// Configuration for Category entity
/// </summary>
public class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("category");
        builder.HasKey(c => c.CategoryId).HasName("category_pk");

        builder.Property(c => c.CategoryId).HasColumnName("category_id");

        builder.Property(c => c.Name)
        .HasColumnName("name")
        .IsRequired().HasMaxLength(100);

        builder.Property(c => c.Description)
        .HasColumnName("description")
        .HasMaxLength(4096);

        builder.Property(c => c.CreatedAt)
        .HasColumnName("created_at");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedArbor.Domain.Entities;

namespace RedArbor.Infrastructure.Data.Configurations;

/// <summary>
/// Configuration for Transaction entity
/// </summary>
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(t => t.TransactionId).HasName("transaction_pk");


        builder.Property(t => t.TransactionId)
            .HasColumnName("transaction_id");

        builder.Property(t => t.Quantity)
        .HasColumnName("quantity")
            .IsRequired();

        builder.Property(t => t.TransactionAt)
            .HasColumnName("transaction_at")
            .IsRequired();

        builder.Property(t => t.TransactionType)
            .HasColumnName("transaction_type")
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

    }
}
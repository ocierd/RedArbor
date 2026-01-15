namespace RedArbor.Domain.Entities;

/// <summary>
/// Product entity
/// </summary>
public class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public short CategoryId { get; set; }
}
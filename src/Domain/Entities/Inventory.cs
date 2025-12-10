namespace RedArbor.Domain.Entities;

public class Inventory
{
    public int InventoryId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int ProductId { get; set; }
}
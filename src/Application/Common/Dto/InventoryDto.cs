namespace RedArbor.Application.Common.Dto;

public class InventoryDto
{
    public int InventoryId { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int ProductId { get; set; }
}
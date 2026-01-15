namespace RedArbor.Domain.Entities;

/// <summary>
/// Category entity
/// </summary>
public class Category
{

    public short CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }


}
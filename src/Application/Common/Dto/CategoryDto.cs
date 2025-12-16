
namespace RedArbor.Application.Common.Dto;
public class CategoryDto
{
    public short CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
}
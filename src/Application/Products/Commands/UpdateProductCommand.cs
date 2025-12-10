namespace RedArbor.Application.Products.Commands;

/// <summary>
/// Command to update a product
/// </summary>
/// <param name="ProductId">Product ID</param>
/// <param name="Name">Name of the product</param>
/// <param name="Description">Description of the product</param>
/// <param name="Price">Price of the product</param>
/// <param name="CategoryId">Category ID of the product</param>
public record UpdateProductCommand(int ProductId, string Name,
string? Description, decimal Price, short CategoryId) : IRequest;
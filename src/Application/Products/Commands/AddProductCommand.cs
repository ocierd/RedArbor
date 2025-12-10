namespace RedArbor.Application.Products.Commands;

/// <summary>
/// Command to add a new product
/// </summary>
/// <param name="Name">Name of the product</param>
/// <param name="Description">Description of the product</param>
/// <param name="Price">Price of the product</param>
/// <param name="CategoryId">Category ID of the product</param>
public record AddProductCommand(string Name, 
string? Description, decimal Price, short CategoryId)
: IRequest<int>;
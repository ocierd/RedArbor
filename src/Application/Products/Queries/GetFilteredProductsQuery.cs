namespace RedArbor.Application.Products.Queries;

/// <summary>
/// Query for retrieving products based on optional filters: Name, CategoryId, MinPrice, and MaxPrice
/// </summary>
/// <param name="Name">Name of product</param>
/// <param name="CategoryId">Category ID of product</param>
/// <param name="MinPrice">Minimum price of product</param>
/// <param name="MaxPrice">Maximum price of product</param>
public record GetFilteredProductsQuery(string? Name, int? CategoryId, int? MinPrice, int? MaxPrice) 
: IRequest<IEnumerable<ProductDto>>;
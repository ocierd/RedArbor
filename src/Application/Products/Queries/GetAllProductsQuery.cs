namespace RedArbor.Application.Products.Queries;


// Query: Get products collection
public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

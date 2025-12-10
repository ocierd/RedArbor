namespace RedArbor.Application.Products.Queries;

public record GetProductByIdQuery(int ProductId) : IRequest<ProductDto>;
using RedArbor.Application.Common.Interfaces.Repository;

namespace RedArbor.Application.Products.Commands.Handlers;

/// <summary>
/// Handler for updating an existing product
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IApplicationDbContext _dbContext;

    public UpdateProductHandler(IProductRepository productRepository, IApplicationDbContext dbContext)
    {
        _productRepository = productRepository;
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateProductCommand request,
    CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FindAsync([request.ProductId], cancellationToken)
            ?? throw new NotFoundException($"Product with Id {request.ProductId} not found.");

        var productDto = new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId
        };

        var updated = await _productRepository.UpdateProductAsync(productDto);
        if (!updated)
        {
            throw new Exception($"Failed to update product with Id {request.ProductId}.");
        }


    }
}
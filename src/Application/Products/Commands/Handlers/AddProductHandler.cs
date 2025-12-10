using RedArbor.Application.Common.Interfaces.Repository;

namespace RedArbor.Application.Products.Commands.Handlers;

/// <summary>
/// Handler for adding a new product
/// </summary>
/// <param name="productRepository">Repository for product data operations</param>
public class AddProductHandler(IProductRepository productRepository)
    : IRequestHandler<AddProductCommand, int>
{

    public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        int productId = await productRepository.AddProductAsync(new ProductDto
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            CategoryId = request.CategoryId
        });

        return productId;
    }
}
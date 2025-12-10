using RedArbor.Application.Common.Interfaces.Repository;

namespace RedArbor.Application.Products.Commands.Handlers;

/// <summary>
/// Handler for deleting a product
/// </summary>
/// <param name="productRepository">The product repository</param>
public class DeleteProductHandler(IProductRepository productRepository) 
    : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository= productRepository;


    public async Task Handle(DeleteProductCommand request
    , CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProductAsync(request.ProductId);
    }
}
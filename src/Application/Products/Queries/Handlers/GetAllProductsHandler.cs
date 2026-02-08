
namespace RedArbor.Application.Products.Queries.Handlers;

/// <summary>
/// Handler for GetAllProductsQuery
/// </summary>
/// <param name="context"> Context for accessing products </param>
public class GetAllProductsHandler(IApplicationDbContext context) 
    : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);
        return products.Select(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CreatedAt = p.CreatedAt
        });
    }
}
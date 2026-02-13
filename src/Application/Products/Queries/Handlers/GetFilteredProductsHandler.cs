namespace RedArbor.Application.Products.Queries.Handlers;

/// <summary>
/// Handler for retrieving products based on filter criteria such as name, catgegory, and price range
/// </summary>
/// <param name="context">Database context for accessing product data   </param>
public class GetFilteredProductsHandler(IApplicationDbContext context) 
    : IRequestHandler<GetFilteredProductsQuery, IEnumerable<ProductDto>>
{
    /// <summary>
    /// Initialize a new instance of ApplicationDbContext for handling product queries
    /// </summary>
    private readonly IApplicationDbContext _context = context;

    /// <summary>
    /// Handle the GetFilteredProductsQuery by applying filters to the products queryable and returning the matching products as a collection of ProductDto
    /// </summary>
    /// <param name="request">Request containing filter criteria</param>
    /// <param name="cancellationToken">Token for cancellation</param>
    /// <returns>Collection of products matching the filter criteria</returns>
    public async Task<IEnumerable<ProductDto>> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(request.Name.ToLower()) 
            || (p.Description !=null && p.Description.ToLower().Contains(request.Name.ToLower())) );
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == request.CategoryId.Value);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= request.MaxPrice.Value);
        }

        var products = await query.ToListAsync(cancellationToken);

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
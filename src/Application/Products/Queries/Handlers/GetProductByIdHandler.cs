using RedArbor.Application.Common.Exceptions;

namespace RedArbor.Application.Products.Queries.Handlers;

/// <summary>
/// Handler for GetProductByIdQuery
/// </summary>
/// <param name="context"> Context for accessing products </param>
public class GetProductByIdHandler(IApplicationDbContext context) 
: IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync([request.ProductId], cancellationToken)
            ?? throw new NotFoundException($"Product with ID {request.ProductId} not found");
        return new ProductDto
        {
            ProductId = entity.ProductId,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            CategoryId = entity.CategoryId
        };
    }
}
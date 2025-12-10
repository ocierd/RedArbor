using Microsoft.EntityFrameworkCore;

namespace RedArbor.Application.Intentory.Queries.Handlers;

public class GetAllInventoriesHandler(IApplicationDbContext context)
: IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryDto>>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<IEnumerable<InventoryDto>> Handle(GetAllInventoriesQuery request
    , CancellationToken cancellationToken)
    {
        var entities = await _context.Inventories.ToListAsync(cancellationToken);

        return entities.Select(entity => new InventoryDto
        {
            InventoryId = entity.InventoryId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        });
    }
}
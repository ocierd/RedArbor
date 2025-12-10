using RedArbor.Application.Common.Exceptions;

namespace RedArbor.Application.Intentory.Queries.Handlers;

public class GetInventoryByIdHandler(IApplicationDbContext context) 
    : IRequestHandler<GetInventoryByIdQuery, InventoryDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<InventoryDto> Handle(GetInventoryByIdQuery request
    , CancellationToken cancellationToken)
    {
        var entity = await _context.Inventories
            .FirstOrDefaultAsync(i => i.InventoryId == request.InventoryId
            , cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException($"Inventory with ID {request.InventoryId} not found");
        }

        return new InventoryDto
        {
            InventoryId = entity.InventoryId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
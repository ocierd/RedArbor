namespace RedArbor.Application.Transactions.Query.Handlers;

/// <summary>
/// Handler for GetTransactionByIdQuery
/// </summary>
/// <param name="context">Context for database access</param>
public class GetTransactionByIdHandler(IApplicationDbContext context) 
    : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Transactions
            .FindAsync([request.TransactionId], cancellationToken) 
            ?? throw new NotFoundException($"Transaction with ID {request.TransactionId} not found");

        return new TransactionDto
        {
            TransactionId = entity.TransactionId,
            ProductId = entity.ProductId,
            Quantity = entity.Quantity,
            TransactionAt = entity.TransactionAt
        };
    }
}
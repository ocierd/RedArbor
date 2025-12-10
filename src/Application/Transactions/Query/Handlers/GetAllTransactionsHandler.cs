
namespace RedArbor.Application.Transactions.Query.Handlers;

public class GetAllTransactionsHandler(IApplicationDbContext context)
: IRequestHandler<GetAllTransactionsQuery, IEnumerable<TransactionDto>>
{
    public async Task<IEnumerable<TransactionDto>> Handle(GetAllTransactionsQuery request
    , CancellationToken cancellationToken)
    {
        var transactions = await context.Transactions
        .ToListAsync(cancellationToken);

        return transactions.Select(t => new TransactionDto
        {
            TransactionId = t.TransactionId,
            Quantity = t.Quantity,
            ProductId = t.ProductId,
            TransactionAt = t.TransactionAt
        });
    }
}
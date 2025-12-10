namespace RedArbor.Application.Transactions.Query;

/// <summary>
/// Query to get transaction by Id
/// </summary>
public record GetTransactionByIdQuery(long TransactionId)
: IRequest<TransactionDto>;
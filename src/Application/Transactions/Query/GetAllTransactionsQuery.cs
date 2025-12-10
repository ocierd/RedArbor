namespace  RedArbor.Application.Transactions.Query;

public record GetAllTransactionsQuery 
: IRequest<IEnumerable<TransactionDto>>;
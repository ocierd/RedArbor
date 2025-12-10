using RedArbor.Application.Transactions.Query;

namespace RedArbor.WebApi.Controllers;
 
 /// <summary>
 /// Controller for handling transactions
 /// </summary>
[ApiController]
[Route("api/[controller]")]
public class TransactionsController (IMediator mediator)
: ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    // Controller actions for transactions would go here


    /// <summary>
    /// Get all transactions
    /// </summary>
    /// <returns>Collection of Transactions</returns>
    [HttpGet]
    [ProducesResponseType<IEnumerable<TransactionDto>>(StatusCodes.Status200OK)]
    public async Task<IEnumerable<TransactionDto>> GetAllTransactions()
    {
        var query = new GetAllTransactionsQuery();
        var result = await _mediator.Send(query);
        return result;
    }


    /// <summary>
    /// Get transaction by Id
    /// </summary>
    /// <param name="id">Transaction Id</param>
    /// <returns>Transaction details or NotFound if not found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType<TransactionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<TransactionDto> GetTransactionById(int id)
    {
        var query = new GetTransactionByIdQuery(id);
        var result = await _mediator.Send(query);
        return result;
    }
}   
namespace RedArbor.Infrastructure.Data.Repository;

using Dapper;
using RedArbor.Application.Common.Interfaces;
using RedArbor.Application.Common.Interfaces.Repository;


/// <summary>
/// Repository for inventory operations
/// </summary>
/// <param name="context">Context for database operations</param>
public class InventoryRepository(IDapperContext context)
: IInventoryRepository
{

    /// <summary>
    /// Checks out a product from inventory
    /// </summary>
    /// <param name="productId">Product id</param>
    /// <param name="quantity">Quantity to ckeckout</param>
    /// <param name="transactionType">Transaction type</param>
    /// <returns>True if the checkout was successful, false otherwise</returns>
    public async Task<bool> CheckoutProductAsync(int productId, int quantity, string transactionType)
    {
        using var connection = context.CreateConnection();
        string sql = "EXEC CheckoutProduct @ProductId, @Quantity, @TransactionType;";
        var parameters = new
        {
            ProductId = productId,
            Quantity = quantity,
            TransactionType = transactionType
        };
        var affected = await connection.ExecuteAsync(sql, parameters);
        return affected > 0;
    }
}
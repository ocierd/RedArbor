namespace RedArbor.Application.Common.Interfaces.Repository;

/// <summary>
/// Interface for inventory repository
/// </summary>
public interface IInventoryRepository
{
    /// <summary>
    /// Checks out a product from inventory
    /// </summary>
    /// <param name="productId">Product id</param>
    /// <param name="quantity">Quantity to ckeckout</param>
    /// <param name="transactionType">Transaction type</param>
    /// <returns>True if the checkout was successful, false otherwise</returns>
    Task<bool> CheckoutProductAsync(int productId, int quantity, string transactionType);
}
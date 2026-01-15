namespace RedArbor.Application.Intentory.Commands.Handlers;

using MediatR;
using RedArbor.Application.Common.Interfaces.Repository;
using RedArbor.Application.Common.Security;
using RedArbor.Application.Intentory.Commands;
using RedArbor.Domain.Constants;

/// <summary>
/// Handler for checking out a product
/// Restricted access to InventoryManager and Administrator roles.
/// </summary>
/// <param name="inventoryRepository">IInventoryRepository instance</param>
/// <returns>Boolean indicating success or failure</returns>
[Authorize(Roles = $"{Roles.InventoryManager},{Roles.Administrator}")]
public class CheckoutProductHandler(IInventoryRepository inventoryRepository)
: IRequestHandler<CheckoutProductCommand, bool>
{
    /// <summary>
    /// Inventory repository
    /// </summary>
    private readonly IInventoryRepository inventoryRepository = inventoryRepository;

    /// <summary>
    /// Handles the checkout product command
    /// </summary>
    /// <param name="request">Checkout product command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Boolean indicating success or failure</returns>
    public async Task<bool> Handle(CheckoutProductCommand request, CancellationToken cancellationToken)
    {
        bool result = await inventoryRepository.CheckoutProductAsync(request.ProductId, request.Quantity, request.TransactionType);
        return result;
    }
}
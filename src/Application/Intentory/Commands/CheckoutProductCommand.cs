namespace RedArbor.Application.Intentory.Commands;

using MediatR;
using RedArbor.Application.Common.Security;
using RedArbor.Domain.Constants;

/// <summary>
/// Command to checkout a product
/// </summary>
/// <param name="ProductId">Product identifier</param>
/// <param name="Quantity">Quantity to checkout</param>
/// <param name="TransactionType">Type of transaction</param>
[Authorize(Roles = $"{Roles.InventoryManager},{Roles.Administrator}")]
[Authorize(Policy = Policies.CanCheckoutProduct)]
public record CheckoutProductCommand(int ProductId, int Quantity, string TransactionType)
: IRequest<bool>;
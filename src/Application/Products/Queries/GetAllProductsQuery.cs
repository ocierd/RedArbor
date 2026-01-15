using RedArbor.Application.Common.Security;
using RedArbor.Domain.Constants;

namespace RedArbor.Application.Products.Queries;


/// <summary>
/// Query to get all products.
/// Access restricted to Administrator with CanGetAllProducts policy.
/// </summary>
[Authorize(Roles = Roles.Administrator)]
[Authorize(Policy = Policies.CanGetAllProducts)]
public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

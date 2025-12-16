using RedArbor.Application.Common.Security;
using RedArbor.Domain.Constants;

namespace RedArbor.Application.Products.Queries;


// Query: Get products collection
[Authorize(Roles = Roles.Administrator)]
public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

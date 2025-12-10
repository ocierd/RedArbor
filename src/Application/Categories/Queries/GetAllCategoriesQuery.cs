namespace RedArbor.Application.Categories.Queries;
public record GetAllCategoriesQuery() : IRequest<IEnumerable<CategoryDto>>;
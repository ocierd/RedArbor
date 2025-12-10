namespace RedArbor.Application.Categories.Queries;

public record GetCategoryByIdQuery(short CategoryId) : IRequest<CategoryDto>;
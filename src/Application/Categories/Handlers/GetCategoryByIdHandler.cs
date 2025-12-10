using RedArbor.Application.Categories.Queries;
using RedArbor.Application.Common.Exceptions;

namespace RedArbor.Application.Categories.Handlers;

/// <summary>
/// Handler to get a category by its ID
/// </summary>
/// <param name="context"> Context for accessing categories </param>
public class GetCategoryByIdHandler(
    IApplicationDbContext context)
    : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await context.Categories
            .FindAsync([request.CategoryId], cancellationToken)
            ?? throw new NotFoundException($"Category with ID {request.CategoryId} not found.");

        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt
        };
    }
}
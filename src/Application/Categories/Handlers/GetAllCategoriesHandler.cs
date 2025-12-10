using Microsoft.EntityFrameworkCore;
using RedArbor.Application.Categories.Queries;

namespace RedArbor.Application.Categories.Handlers;

/// <summary>
/// Handler for getting all categories
/// </summary>
/// <param name="context">Context for accessing the categories</param>
public class GetAllCategoriesHandler(
    IApplicationDbContext context)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await context.Categories
        .Select(c => new CategoryDto
        {
            CategoryId = c.CategoryId,
            Name = c.Name,
            Description = c.Description,
            CreatedAt = c.CreatedAt
        })
        .ToListAsync(cancellationToken);

        return categories;
    }
}
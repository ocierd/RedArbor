using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace RedArbor.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IMediator mediator)
: ApiControllerBase
{
    // Controller actions...
    private readonly IMediator _mediator = mediator;


    [HttpGet]
    [ProducesResponseType<IEnumerable<CategoryDto>>(StatusCodes.Status200OK)]
    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        
        var response = await _mediator.Send(new GetAllCategoriesQuery());

        return response;
    }


    [HttpGet("{id}")]
    [ProducesResponseType<CategoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CategoryDto> GetCategoryById(short id)
    {
        var response = await _mediator.Send(new GetCategoryByIdQuery(id));
        return response;
    }
}
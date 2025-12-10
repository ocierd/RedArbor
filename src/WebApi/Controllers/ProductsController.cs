using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedArbor.Application.Dto;
using RedArbor.Application.Products.Queries;

namespace RedArbor.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;



    [HttpGet()]
    [ProducesResponseType<IEnumerable<ProductDto>>(StatusCodes.Status200OK)]
    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var response = await _mediator.Send(new GetAllProductsQuery());
        return response;
    }


    [HttpGet("{id}")]
    [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ProductDto> GetProductById(int id)
    {
        var response = await _mediator.Send(new GetProductByIdQuery(id));
        return response;
    }

}

using RedArbor.Application.Products.Commands;
using RedArbor.Application.Products.Queries;

namespace RedArbor.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ApiControllerBase
{
    private readonly IMediator _mediator = mediator;



    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>Collection of all products</returns>
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductDto>>(StatusCodes.Status200OK)]
    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var response = await _mediator.Send(new GetAllProductsQuery());
        return response;
    }


    /// <summary>
    /// Get products filtered by name, category, price range
    /// </summary>
    /// <param name="query">Filter criteria</param>
    /// <returns>Collection of filtered products</returns>
    [HttpPost("filtered")]
    public async Task<IEnumerable<ProductDto>> GetFilteredProducts(
        [FromBody] GetFilteredProductsQuery query)
    {
        var response = await _mediator.Send(query);
        return response;
    }

    /// <summary>
    /// / Get product by Id
    /// </summary>
    /// <param name="id">Product id</param>
    /// <returns>Product or NotFound</returns>
    [HttpGet("{id}")]
    [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ProductDto> GetProductById(int id)
    {
        var response = await _mediator.Send(new GetProductByIdQuery(id));
        return response;
    }

    /// <summary>
    /// Add a new product
    /// </summary>
    /// <param name="command">Coommand paramters</param>
    /// <returns>Id of the newly added product</returns>
    [HttpPost]
    [ProducesResponseType<int>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<int> AddProduct([FromBody] AddProductCommand command)
    {
        var productId = await _mediator.Send(command);
        return productId;
    }




    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">Id of product</param>
    /// <param name="command">Command arguments</param>
    /// <returns>NoContent or BadRequest</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
    {
        if (command.ProductId <= 0)
        {
            return BadRequest("Product ID doesn't exist.");
        }

        await _mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    /// Delete a product by Id
    /// </summary>
    /// <param name="id">Id of product</param>
    /// <returns>NoContent</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

}

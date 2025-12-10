namespace RedArbor.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController (IMediator mediator): ApiControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ProducesResponseType<IEnumerable<InventoryDto>>(StatusCodes.Status200OK)]
    public async Task<IEnumerable<InventoryDto>> GetAllInventories()
    {
        var query = new GetAllInventoriesQuery();
        var result = await _mediator.Send(query);
        return result;
    }


    [HttpGet("{id}")]
    [ProducesResponseType<InventoryDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<InventoryDto> GetInventoryById(int id)
    {
        var query = new GetInventoryByIdQuery(id);
        var result = await _mediator.Send(query);
        return result;
    }
}
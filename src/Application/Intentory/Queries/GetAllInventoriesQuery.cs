namespace RedArbor.Application.Intentory.Queries;
 
 /// <summary>
 /// Query to get all inventories
 /// </summary> 
public record GetAllInventoriesQuery() : IRequest<IEnumerable<InventoryDto>>;
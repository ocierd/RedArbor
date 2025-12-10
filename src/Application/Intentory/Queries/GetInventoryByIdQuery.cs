namespace RedArbor.Application.Intentory.Queries;
 
 /// <summary>
 /// Query to get inventory by Id
 /// </summary>
public record GetInventoryByIdQuery(int InventoryId) 
: IRequest<InventoryDto>;
namespace RedArbor.Application.Products.Commands;

public record DeleteProductCommand(int ProductId) : IRequest;
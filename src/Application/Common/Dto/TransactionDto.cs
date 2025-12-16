namespace RedArbor.Application.Common.Dto;

public class TransactionDto
{
    public long TransactionId { get; set; }

    public int Quantity { get; set; }

    public DateTime TransactionAt { get; set; }

    public string TransactionType { get; set; } = null!;

    public int ProductId { get; set; }
}
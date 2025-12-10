using System.ComponentModel.DataAnnotations;

namespace RedArbor.Domain.Entities;

public class Transaction
{
    public long TransactionId { get; set; }

    public int Quantity { get; set; }

    public DateTime TransactionAt { get; set; }

    [AllowedValues("Selled", "Shrinkage", "Discontinued")]
    public string TransactionType { get; set; } = null!;

    public int ProductId { get; set; }
}
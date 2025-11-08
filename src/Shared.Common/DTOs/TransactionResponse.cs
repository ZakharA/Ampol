namespace Shared.Common.DTOs;

public class TransactionResponse
{
    public string CustomerId { get; set; } = string.Empty;
    public string LoyaltyCard { get; set; } = string.Empty;
    public DateTimeOffset TransactionDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountApplied { get; set; }
    public decimal GrandTotal { get; set; }
    public int PointsEarned { get; set; }
}
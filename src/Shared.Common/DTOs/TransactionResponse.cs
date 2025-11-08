namespace Shared.Common.DTOs;

public class TransactionResponse
{
    public string CustomerId { get; set; } = string.Empty;
    public string LoyaltyCard { get; set; } = string.Empty;
    public string TransactionDate { get; set; } = string.Empty;
    public string TotalAmount { get; set; } = string.Empty;
    public string DiscountApplied { get; set; } = string.Empty;
    public string GrandTotal { get; set; } = string.Empty;
    public string PointsEarned { get; set; } = string.Empty;
}
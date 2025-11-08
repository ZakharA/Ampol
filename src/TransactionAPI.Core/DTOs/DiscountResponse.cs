namespace TransactionAPI.Core.DTOs;

public class DiscountResponse
{
    public string TotalAmount { get; set; } = string.Empty;
    public string DiscountApplied { get; set; } = string.Empty;
    public string GrandTotal { get; set; } = string.Empty;
}
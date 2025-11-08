namespace TransactionAPI.Core.DTOs;

public class DiscountResponse
{
    public decimal TotalAmount { get; set; }
    public decimal DiscountApplied { get; set; }
    public decimal GrandTotal { get; set; }
}
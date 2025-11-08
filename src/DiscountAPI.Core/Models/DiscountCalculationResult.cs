namespace DiscountAPI.Core.Models;

public class DiscountCalculationResult
{
    public decimal TotalAmount { get; set; }
    public decimal DiscountApplied { get; set; }
    public decimal GrandTotal { get; set; }
    public List<AppliedDiscount> AppliedDiscounts { get; set; } = new();
}

public class AppliedDiscount
{
    public string DiscountName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
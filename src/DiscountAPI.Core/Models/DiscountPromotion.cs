namespace DiscountAPI.Core.Models;

public class DiscountPromotion
{
    public string Id { get; set; } = string.Empty;
    public string PromotionName { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal DiscountPercent { get; set; }
}
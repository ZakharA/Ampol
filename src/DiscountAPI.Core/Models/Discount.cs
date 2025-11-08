namespace DiscountAPI.Core.Models;

public class Discount
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public decimal DiscountPercentage { get; set; }
}
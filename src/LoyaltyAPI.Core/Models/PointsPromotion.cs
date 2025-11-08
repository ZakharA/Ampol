using Shared.Common.Models;

namespace LoyaltyAPI.Core.Models;

public class PointsPromotion
{
    public string Id { get; set; } = string.Empty;
    public string PromotionName { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public ProductCategory? Category { get; set; }
    public int PointsPerDollar { get; set; }
}
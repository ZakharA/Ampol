using Shared.Common.Models;

namespace LoyaltyAPI.Core.Models;

public class PointsPromotion
{
    public string Id { get; set; } = string.Empty;
    public string PromotionName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProductCategory Category { get; set; } = ProductCategory.None;
    public int PointsPerDollar { get; set; }
}
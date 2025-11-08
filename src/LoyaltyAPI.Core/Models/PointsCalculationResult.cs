using Shared.Common.Models;

namespace LoyaltyAPI.Core.Models;

public class PointsCalculationResult
{
    public int TotalPoints { get; set; }
    public string PromotionApplied { get; set; } = string.Empty;
    public Dictionary<ProductCategory, int> PointsByCategory { get; set; } = new();
}
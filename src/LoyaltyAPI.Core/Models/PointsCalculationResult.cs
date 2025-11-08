using Shared.Common.Models;

namespace LoyaltyAPI.Core.Models;

public class PointsCalculationResult
{
    public int PointsEarned { get; set; }
    public string PromotionApplied { get; set; } = string.Empty;
}
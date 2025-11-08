namespace LoyaltyAPI.DTOs;

public class PointsResponse
{
    public int PointsEarned { get; set; }
    public string PromotionApplied { get; set; } = string.Empty;
}
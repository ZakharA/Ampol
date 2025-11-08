using LoyaltyAPI.Core.Models;

namespace LoyaltyAPI.Core.Interfaces;

public interface IPointsPromotionRepository
{
    Task<PointsPromotion?> GetPointsPromotionAsync(DateTimeOffset transactionDate);
}
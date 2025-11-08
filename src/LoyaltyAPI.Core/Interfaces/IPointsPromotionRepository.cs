using LoyaltyAPI.Core.Models;

namespace LoyaltyAPI.Core.Interfaces;

public interface IPointsPromotionRepository
{
    Task<PointsPromotion?> GetActivePromotionAsync(DateTime transactionDate);
    Task<IEnumerable<PointsPromotion>> GetAllPromotionsAsync();
}
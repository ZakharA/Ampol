using DiscountAPI.Core.Models;

namespace DiscountAPI.Core.Interfaces;

public interface IDiscountPromotionRepository
{
    Task<DiscountPromotion?> GetDiscountPromotionAsync(DateTimeOffset transactionDate);
    Task<IEnumerable<DiscountPromotionProduct>> GetDiscountPromotionProductsAsync(string discountPromotionId);
}
using DiscountAPI.Core.Interfaces;
using DiscountAPI.Core.Models;

namespace DiscountAPI.Infra.Repositories;

public class DiscountPromotionRepository : IDiscountPromotionRepository
{
    private static readonly List<DiscountPromotion> _promotions =
    [
        new()
        {
            Id = "DP001",
            PromotionName = "Fuel Discount Promo",
            StartDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EndDate = new DateTimeOffset(2020, 2, 15, 0, 0, 0, TimeSpan.Zero),
            DiscountPercent = 20
        },
        new()
        {
            Id = "DP002",
            PromotionName = "Happy Promo",
            StartDate = new DateTimeOffset(2020, 3, 2, 0, 0, 0, TimeSpan.Zero),
            EndDate = new DateTimeOffset(2020, 3, 20, 0, 0, 0, TimeSpan.Zero),
            DiscountPercent = 15
        }
    ];

    private static readonly List<DiscountPromotionProduct> _promotionProducts =
    [
        new() { DiscountPromotionId = "DP001", ProductId = "PRD02" }
    ];

    public Task<DiscountPromotion?> GetDiscountPromotionAsync(DateTimeOffset transactionDate)
    {
        var promotion = _promotions.FirstOrDefault(p =>
            transactionDate >= p.StartDate && transactionDate <= p.EndDate);
        return Task.FromResult(promotion);
    }

    public Task<IEnumerable<DiscountPromotionProduct>> GetDiscountPromotionProductsAsync(string discountPromotionId)
    {
        var products = _promotionProducts.Where(p => p.DiscountPromotionId == discountPromotionId);
        return Task.FromResult(products);
    }
}
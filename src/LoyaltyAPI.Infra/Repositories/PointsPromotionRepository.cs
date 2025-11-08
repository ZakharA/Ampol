using LoyaltyAPI.Core.Interfaces;
using LoyaltyAPI.Core.Models;
using Shared.Common.Models;

namespace LoyaltyAPI.Infra.Repositories;

public class PointsPromotionRepository : IPointsPromotionRepository
{
    private static readonly List<PointsPromotion> _promotions =
    [
        new()
        {
            Id = "PP001",
            PromotionName = "New Year Promo",
            StartDate = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero),
            EndDate = new DateTimeOffset(2020, 1, 30, 0, 0, 0, TimeSpan.Zero),
            Category = null,
            PointsPerDollar = 2
        },
        new()
        {
            Id = "PP002",
            PromotionName = "Fuel Promo",
            StartDate = new DateTimeOffset(2020, 2, 5, 0, 0, 0, TimeSpan.Zero),
            EndDate = new DateTimeOffset(2020, 2, 15, 0, 0, 0, TimeSpan.Zero),
            Category = ProductCategory.Fuel,
            PointsPerDollar = 3
        },
        new()
        {
            Id = "PP003",
            PromotionName = "Shop Promo",
            StartDate = new DateTimeOffset(2020, 3, 1, 0, 0, 0, TimeSpan.Zero),
            EndDate = new DateTimeOffset(2020, 3, 20, 0, 0, 0, TimeSpan.Zero),
            Category = ProductCategory.Shop,
            PointsPerDollar = 4
        }
    ];

    public Task<PointsPromotion?> GetPointsPromotionAsync(DateTimeOffset transactionDate)
    {
        var promotion = _promotions.FirstOrDefault(p =>
            transactionDate >= p.StartDate && transactionDate <= p.EndDate);
        return Task.FromResult(promotion);
    }
}
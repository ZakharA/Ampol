using LoyaltyAPI.Core.Interfaces;
using LoyaltyAPI.Core.Models;
using Shared.Common.Models;

namespace LoyaltyAPI.Core.Services;

public class LoyaltyService : ILoyaltyService
{
    private readonly IPointsPromotionRepository _pointsPromotionRepository;
    private readonly IProductRepository _productRepository;

    public LoyaltyService(
        IPointsPromotionRepository pointsPromotionRepository,
        IProductRepository productRepository)
    {
        _pointsPromotionRepository = pointsPromotionRepository;
        _productRepository = productRepository;
    }

    public async Task<PointsCalculationResult> CalculatePointsAsync(
        List<BasketItem> basket,
        decimal grandTotal,
        DateTimeOffset transactionDate)
    {
        var pointsEarned = 0;
        var promotionApplied = string.Empty;

        var promotion = await _pointsPromotionRepository.GetPointsPromotionAsync(transactionDate);
        if (promotion != null)
        {
            promotionApplied = promotion.PromotionName;
            if (promotion.Category.HasValue)
            {
                var totalSpentOnCategory = 0m;
                foreach (var item in basket)
                {
                    var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                    if (product != null && product.Category == promotion.Category.Value)
                    {
                        totalSpentOnCategory += item.UnitPrice * item.Quantity;
                    }
                }
                pointsEarned = (int)(totalSpentOnCategory * promotion.PointsPerDollar);
            }
            else
            {
                pointsEarned = (int)(grandTotal * promotion.PointsPerDollar);
            }
        }

        return new PointsCalculationResult
        {
            PointsEarned = pointsEarned,
            PromotionApplied = promotionApplied
        };
    }
}
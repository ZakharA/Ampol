using LoyaltyAPI.Core.Interfaces;
using LoyaltyAPI.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Shared.Common.Models;

namespace LoyaltyAPI.Core.Services;

public class LoyaltyService : ILoyaltyService
{
    private readonly IPointsPromotionRepository _pointsPromotionRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    public LoyaltyService(
        IPointsPromotionRepository pointsPromotionRepository,
        IProductRepository productRepository,
        IMemoryCache cache)
    {
        _pointsPromotionRepository = pointsPromotionRepository;
        _productRepository = productRepository;
        _cache = cache;

        // Pre-cache all products on service initialization
        CacheAllProducts();
    }

    private void CacheAllProducts()
    {
        _cache.GetOrCreate("all_products", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return _productRepository.GetAllProductsAsync().Result;
        });
    }

    public async Task<PointsCalculationResult> CalculatePointsAsync(
        List<BasketItem> basket,
        decimal grandTotal,
        DateTimeOffset transactionDate)
    {
        var pointsEarned = 0;
        var promotionApplied = string.Empty;

        var promotion = await _cache.GetOrCreateAsync(
            $"promotion_{transactionDate:yyyyMMdd}",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return await _pointsPromotionRepository.GetPointsPromotionAsync(transactionDate);
            });

        if (promotion != null)
        {
            promotionApplied = promotion.PromotionName;
            if (promotion.Category.HasValue)
            {
                var totalSpentOnCategory = 0m;
                var allProducts = _cache.Get<IEnumerable<Product>>("all_products");
                foreach (var item in basket)
                {
                    var product = allProducts?.FirstOrDefault(p => p.ProductId == item.ProductId);
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
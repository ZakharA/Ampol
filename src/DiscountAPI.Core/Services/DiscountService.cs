using DiscountAPI.Core.Interfaces;
using DiscountAPI.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Shared.Common.Models;

namespace DiscountAPI.Core.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountPromotionRepository _discountPromotionRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    public DiscountService(
        IDiscountPromotionRepository discountPromotionRepository,
        IProductRepository productRepository,
        IMemoryCache cache)
    {
        _discountPromotionRepository = discountPromotionRepository;
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

    public async Task<DiscountCalculationResult> CalculateDiscountAsync(
        List<BasketItem> basket,
        DateTimeOffset transactionDate)
    {
        var totalAmount = basket.Sum(item => item.UnitPrice * item.Quantity);
        var discountApplied = 0m;

        var promotion = await _cache.GetOrCreateAsync(
            $"promotion_{transactionDate:yyyyMMdd}",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return await _discountPromotionRepository.GetDiscountPromotionAsync(transactionDate);
            });

        if (promotion != null)
        {
            var promotionProducts = await _cache.GetOrCreateAsync(
                $"promotion_products_{promotion.Id}",
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return await _discountPromotionRepository.GetDiscountPromotionProductsAsync(promotion.Id);
                });
            var promotionProductIds = promotionProducts?.Select(p => p.ProductId).ToHashSet() ?? new HashSet<string>();
            var allProducts = _cache.Get<IEnumerable<Product>>("all_products") ?? Enumerable.Empty<Product>();

            foreach (var item in basket)
            {
                if (promotionProductIds.Contains(item.ProductId))
                {
                    var product = allProducts?.FirstOrDefault(p => p.ProductId == item.ProductId);
                    if (product != null)
                    {
                        discountApplied += item.UnitPrice * item.Quantity * (promotion.DiscountPercent / 100);
                    }
                }
            }
        }

        var grandTotal = totalAmount - discountApplied;

        return new DiscountCalculationResult
        {
            TotalAmount = totalAmount,
            DiscountApplied = discountApplied,
            GrandTotal = grandTotal
        };
    }
}
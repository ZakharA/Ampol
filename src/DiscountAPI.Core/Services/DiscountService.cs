using DiscountAPI.Core.Interfaces;
using DiscountAPI.Core.Models;
using Shared.Common.Models;

namespace DiscountAPI.Core.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountPromotionRepository _discountPromotionRepository;
    private readonly IProductRepository _productRepository;

    public DiscountService(
        IDiscountPromotionRepository discountPromotionRepository,
        IProductRepository productRepository)
    {
        _discountPromotionRepository = discountPromotionRepository;
        _productRepository = productRepository;
    }

    public async Task<DiscountCalculationResult> CalculateDiscountAsync(
        List<BasketItem> basket,
        DateTimeOffset transactionDate)
    {
        var totalAmount = basket.Sum(item => item.UnitPrice * item.Quantity);
        var discountApplied = 0m;

        var promotion = await _discountPromotionRepository.GetDiscountPromotionAsync(transactionDate);
        if (promotion != null)
        {
            var promotionProducts = await _discountPromotionRepository.GetDiscountPromotionProductsAsync(promotion.Id);
            var promotionProductIds = promotionProducts.Select(p => p.ProductId).ToHashSet();

            foreach (var item in basket)
            {
                if (promotionProductIds.Contains(item.ProductId))
                {
                    var product = await _productRepository.GetProductByIdAsync(item.ProductId);
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
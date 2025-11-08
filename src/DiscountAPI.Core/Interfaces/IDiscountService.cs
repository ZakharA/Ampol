using DiscountAPI.Core.Models;
using Shared.Common.Models;

namespace DiscountAPI.Core.Interfaces;

public interface IDiscountService
{
    Task<DiscountCalculationResult> CalculateDiscountAsync(
        List<BasketItem> basket,
        DateTimeOffset transactionDate);
}
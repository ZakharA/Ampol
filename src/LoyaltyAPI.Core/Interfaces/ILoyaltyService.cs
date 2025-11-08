using LoyaltyAPI.Core.Models;
using Shared.Common.Models;

namespace LoyaltyAPI.Core.Interfaces;

public interface ILoyaltyService
{
    Task<PointsCalculationResult> CalculatePointsAsync(
        List<BasketItem> basket,
        decimal grandTotal,
        DateTimeOffset transactionDate);
}

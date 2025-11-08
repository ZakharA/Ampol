using Shared.Common.DTOs;
using TransactionAPI.Core.Interfaces;

namespace TransactionAPI.Core.Services;

public class TransactionService : ITransactionService
{
    private readonly IDiscountService _discountService;
    private readonly ILoyaltyService _loyaltyService;

    public TransactionService(
        IDiscountService discountService,
        ILoyaltyService loyaltyService)
    {
        _discountService = discountService;
        _loyaltyService = loyaltyService;
    }

    public async Task<TransactionResponse?> ProcessTransactionAsync(TransactionRequest request)
    {
        var discountResponse = await _discountService.CalculateDiscountAsync(request);
        if (discountResponse == null)
        {
            return null;
        }

        var pointsResponse = await _loyaltyService.CalculatePointsAsync(request, discountResponse.GrandTotal);
        if (pointsResponse == null)
        {
            return null;
        }

        return new TransactionResponse
        {
            CustomerId = request.CustomerId,
            LoyaltyCard = request.LoyaltyCard,
            TransactionDate = DateTimeOffset.Parse(request.TransactionDate),
            TotalAmount = discountResponse.TotalAmount,
            DiscountApplied = discountResponse.DiscountApplied,
            GrandTotal = discountResponse.GrandTotal,
            PointsEarned = pointsResponse.PointsEarned
        };
    }
}
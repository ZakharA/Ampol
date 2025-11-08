using Shared.Common.DTOs;
using TransactionAPI.Core.DTOs;

namespace TransactionAPI.Core.Interfaces;

public interface ILoyaltyService
{
    Task<PointsResponse?> CalculatePointsAsync(TransactionRequest request, string grandTotal);
}
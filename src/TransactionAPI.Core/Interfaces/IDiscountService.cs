using Shared.Common.DTOs;
using TransactionAPI.Core.DTOs;

namespace TransactionAPI.Core.Interfaces;

public interface IDiscountService
{
    Task<DiscountResponse?> CalculateDiscountAsync(TransactionRequest request);
}
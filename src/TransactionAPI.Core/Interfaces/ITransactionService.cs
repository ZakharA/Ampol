using Shared.Common.DTOs;

namespace TransactionAPI.Core.Interfaces;

public interface ITransactionService
{
    Task<TransactionResponse?> ProcessTransactionAsync(TransactionRequest request);
}
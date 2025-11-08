namespace Shared.Common.DTOs;

public class TransactionRequest
{
    public string CustomerId { get; set; } = string.Empty;
    public string LoyaltyCard { get; set; } = string.Empty;
    public string TransactionDate { get; set; } = string.Empty;
    public List<BasketItemDto> Basket { get; set; } = [];
}
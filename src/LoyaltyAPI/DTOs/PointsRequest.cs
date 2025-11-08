namespace LoyaltyAPI.DTOs;

public class PointsRequest
{
    public string TransactionDate { get; set; } = string.Empty;
    public decimal GrandTotal { get; set; } = decimal.Zero;
    public List<BasketItemDto> Basket { get; set; } = [];
}
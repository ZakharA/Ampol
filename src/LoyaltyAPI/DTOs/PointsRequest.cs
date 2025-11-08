namespace LoyaltyAPI.DTOs;

public class PointsRequest
{
    public required DateTimeOffset TransactionDate { get; set; }
    public decimal GrandTotal { get; set; } = decimal.Zero;
    public List<BasketItemDto> Basket { get; set; } = [];
}
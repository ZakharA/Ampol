namespace LoyaltyAPI.DTOs;

public class BasketItemDto
{
    public string ProductId { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; } = decimal.Zero;
    public int Quantity { get; set; } = 0;
}

namespace Shared.Common.DTOs;

public class BasketItemDto
{
    public required string ProductId { get; set; }
    public required decimal UnitPrice { get; set; }
    public required int Quantity { get; set; }
}
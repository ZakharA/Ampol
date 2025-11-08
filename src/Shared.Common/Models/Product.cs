namespace Shared.Common.Models;

public class Product
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public ProductCategory Category { get; set; }
    public decimal UnitPrice { get; set; }
}
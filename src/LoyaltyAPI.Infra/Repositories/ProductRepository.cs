using LoyaltyAPI.Core.Interfaces;
using Shared.Common.Models;

namespace LoyaltyAPI.Infra.Repositories;

public class ProductRepository : IProductRepository
{
    private static readonly List<Product> _products =
    [
        new() { ProductId = "PRD01", ProductName = "Vortex 95", Category = ProductCategory.Fuel, UnitPrice = 1.2m },
        new() { ProductId = "PRD02", ProductName = "Vortex 98", Category = ProductCategory.Fuel, UnitPrice = 1.3m },
        new() { ProductId = "PRD03", ProductName = "Diesel", Category = ProductCategory.Fuel, UnitPrice = 1.1m },
        new() { ProductId = "PRD04", ProductName = "Twix 55g", Category = ProductCategory.Shop, UnitPrice = 2.3m },
        new() { ProductId = "PRD05", ProductName = "Mars 72g", Category = ProductCategory.Shop, UnitPrice = 5.1m },
        new() { ProductId = "PRD06", ProductName = "SNICKERS 72G", Category = ProductCategory.Shop, UnitPrice = 3.4m },
        new() { ProductId = "PRD07", ProductName = "Bounty 3 63g", Category = ProductCategory.Shop, UnitPrice = 6.9m },
        new() { ProductId = "PRD08", ProductName = "Snickers 50g", Category = ProductCategory.Shop, UnitPrice = 4.0m }
    ];

    public Task<Product?> GetProductByIdAsync(string productId)
    {
        var product = _products.FirstOrDefault(p => p.ProductId == productId);
        return Task.FromResult(product);
    }
}
using Shared.Common.Models;

namespace LoyaltyAPI.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(string productId);
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
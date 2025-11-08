using Shared.Common.Models;

namespace DiscountAPI.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(string productId);
    Task<IEnumerable<Product>> GetAllProductsAsync();
}
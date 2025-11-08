using LoyaltyAPI.DTOs;
using Shared.Common.Models;

namespace LoyaltyAPI.Mapping
{
    public static class BasketMapping
    {
        public static List<BasketItem> ToDomain(this List<BasketItemDto> dtos)
        {
            return [.. dtos.Select(dto => new BasketItem
            {
                ProductId = dto.ProductId,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity
            })];
        }
    }
}
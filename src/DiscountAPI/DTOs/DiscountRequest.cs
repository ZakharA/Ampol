using Shared.Common.DTOs;
using System.Collections.Generic;

namespace DiscountAPI.DTOs
{
    public class DiscountRequest
    {
        public required List<BasketItemDto> Basket { get; set; }
        public required DateTimeOffset TransactionDate { get; set; }
    }
}
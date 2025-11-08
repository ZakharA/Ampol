namespace DiscountAPI.DTOs
{
    public class DiscountResponse
    {
        public required decimal TotalAmount { get; set; }
        public required decimal DiscountApplied { get; set; }
        public required decimal GrandTotal { get; set; }
    }
}
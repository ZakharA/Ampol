namespace DiscountAPI.DTOs
{
    public class DiscountResponse
    {
        public required string TotalAmount { get; set; }
        public required string DiscountApplied { get; set; }
        public required string GrandTotal { get; set; }
    }
}
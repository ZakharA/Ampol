using FluentValidation;
using LoyaltyAPI.DTOs;

namespace LoyaltyAPI.Validators
{
    public class PointsRequestValidator : AbstractValidator<PointsRequest>
    {
        public PointsRequestValidator()
        {
            RuleFor(x => x.Basket).NotEmpty();
            RuleForEach(x => x.Basket).SetValidator(new BasketItemDtoValidator());
            RuleFor(x => x.GrandTotal).GreaterThan(0);
            RuleFor(x => x.TransactionDate).NotEmpty().LessThanOrEqualTo(DateTimeOffset.UtcNow);
        }
    }

    public class BasketItemDtoValidator : AbstractValidator<BasketItemDto>
    {
        public BasketItemDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.UnitPrice).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
using FluentValidation;
using DiscountAPI.DTOs;
using Shared.Common.DTOs;

namespace DiscountAPI.Validators
{
    public class DiscountRequestValidator : AbstractValidator<DiscountRequest>
    {
        public DiscountRequestValidator()
        {
            RuleFor(x => x.Basket).NotEmpty();
            RuleForEach(x => x.Basket).SetValidator(new BasketItemDtoValidator());
            RuleFor(x => x.TransactionDate).NotEmpty().LessThanOrEqualTo(DateTimeOffset.UtcNow)
                .WithMessage("Transaction date cannot be in the future.");
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
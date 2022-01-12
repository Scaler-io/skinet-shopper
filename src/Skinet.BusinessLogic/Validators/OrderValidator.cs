using FluentValidation;
using Skinet.BusinessLogic.Core.Dtos.OrderingDtos;

namespace Skinet.BusinessLogic.Validators
{
    public class OrderValidator : AbstractValidator<OrderRequestDto>
    {
        public OrderValidator()
        {
            RuleFor(o => o.basketId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Basket id is required")
                .NotNull()
                .WithMessage("Basket id is required");

            RuleFor(o => o.DeliveryMethodId)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Deleivery method is required")
                .NotNull();

            RuleFor(o => o.ShippingAddress)
                .NotNull()
                .WithMessage("Shipping address is required")
                .SetValidator(new UserAddressValidator());
        }
    }
}
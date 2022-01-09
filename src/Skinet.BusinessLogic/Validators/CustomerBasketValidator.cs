using FluentValidation;
using Skinet.BusinessLogic.Core.Dtos.Basket;

namespace Skinet.BusinessLogic.Validators
{
    public class CustomerBasketValidator : AbstractValidator<CustomerBasketDto>
    {
        public CustomerBasketValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Basket id is required")
                .NotNull();
        }
    }
}

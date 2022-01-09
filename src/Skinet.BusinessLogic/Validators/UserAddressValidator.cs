using FluentValidation;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;


namespace Skinet.BusinessLogic.Validators
{
    public class UserAddressValidator : AbstractValidator<UserAddressDto>
    {
        public UserAddressValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("First name is required.")
                .NotNull()
                .MaximumLength(50)
                .WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .NotNull()
                .MaximumLength(50)
                .WithMessage("Last name must not exceed 50 characters.");

            RuleFor(x => x.Street)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Street name is required.")
                .NotNull();

            RuleFor(x => x.City)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("City name is required.")
                .NotNull();

            RuleFor(x => x.State)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("State is required.")
                .NotNull();

            RuleFor(x => x.ZipCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Zip code is required.")
                .NotNull();

        }
    }
}

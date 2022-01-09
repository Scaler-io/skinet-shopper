using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Skinet.BusinessLogic.Core.Dtos.IdentityDtos;
using Skinet.Entities.Entities.Identity;

namespace Skinet.BusinessLogic.Validators
{
    public class SignupRequestValidtor : AbstractValidator<SignupRequestDto>
    {
        private readonly UserManager<SkinetUser> _userManager;
        public SignupRequestValidtor(UserManager<SkinetUser> userManager)
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .WithMessage("Display name is required")
                .NotNull();

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email is required")
                .NotNull()
                .EmailAddress()
                .WithMessage("Please enter a valid email address");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password is required")
                .NotNull()
                .Matches("(?=^.{6,15}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$")
                .WithMessage("Please select a strong password");
            
            _userManager = userManager;
        }

    }
}

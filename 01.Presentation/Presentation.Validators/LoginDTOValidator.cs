using Domain.DTOs.Account;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Presentation.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator(UserManager<IdentityUser> userManager)
        {
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsDigit))
                .WithMessage(ValidationCode.HasDigit.ToString());
            RuleFor(r => r.Password)
                .MinimumLength(6)
                .WithMessage(ValidationCode.MinimumLength.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsLower))
                .WithMessage(ValidationCode.HasLowerCase.ToString());
            RuleFor(r => r.Password)
                .Must(p => !p.All(char.IsLetterOrDigit))
                .WithMessage(ValidationCode.HasNonAlphanumeric.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsUpper))
                .WithMessage(ValidationCode.HasUpperCase.ToString());

            RuleFor(r => r.Identifier)
                .NotEmpty()
                .WithMessage(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Identifier)
                .MustAsync(async (identifier, _) => (await userManager.FindByEmailAsync(identifier) ?? await userManager.FindByNameAsync(identifier)) != null)
                .WithMessage(ValidationCode.IdentifierExists.ToString());
        }
    }
}
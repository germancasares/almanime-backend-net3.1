using Domain.DTOs.Account;
using Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Presentation.Validators.FluentValidation
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator(UserManager<IdentityUser> userManager)
        {
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage(EValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsDigit))
                .WithMessage(EValidationCode.HasDigit.ToString());
            RuleFor(r => r.Password)
                .MinimumLength(6)
                .WithMessage(EValidationCode.MinimumLength.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsLower))
                .WithMessage(EValidationCode.HasLowerCase.ToString());
            RuleFor(r => r.Password)
                .Must(p => !p.All(char.IsLetterOrDigit))
                .WithMessage(EValidationCode.HasNonAlphanumeric.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsUpper))
                .WithMessage(EValidationCode.HasUpperCase.ToString());

            RuleFor(r => r.Identifier)
                .NotEmpty()
                .WithMessage(EValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Identifier)
                .MustAsync(async (identifier, _) => (await userManager.FindByEmailAsync(identifier) ?? await userManager.FindByNameAsync(identifier)) != null)
                .WithMessage(EValidationCode.IdentifierExists.ToString());
        }
    }
}
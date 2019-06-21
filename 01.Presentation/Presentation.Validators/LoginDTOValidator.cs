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
                .WithErrorCode(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsDigit))
                .WithErrorCode(ValidationCode.HasDigit.ToString());
            RuleFor(r => r.Password)
                .MinimumLength(6)
                .WithErrorCode(ValidationCode.MinimumLength.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsLower))
                .WithErrorCode(ValidationCode.HasLowerCase.ToString());
            RuleFor(r => r.Password)
                .Must(p => !p.All(char.IsLetterOrDigit))
                .WithErrorCode(ValidationCode.HasNonAlphanumeric.ToString());
            RuleFor(r => r.Password)
                .Must(p => p.Any(char.IsUpper))
                .WithErrorCode(ValidationCode.HasUpperCase.ToString());

            RuleFor(r => r.Identifier)
                .NotEmpty()
                .WithErrorCode(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Identifier)
                .MustAsync(async (identifier, _) => (await userManager.FindByEmailAsync(identifier) ?? await userManager.FindByNameAsync(identifier)) != null)
                .WithErrorCode(ValidationCode.IdentifierExists.ToString());
        }
    }
}
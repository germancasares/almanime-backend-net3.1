using Application.Interfaces;
using Domain.DTOs.Account;
using FluentValidation;
using System.Linq;

namespace Presentation.Validators
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator(IAccountService accountService)
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

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithErrorCode(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Email)
                .EmailAddress()
                .WithErrorCode(ValidationCode.ValidEmailAddress.ToString());
            RuleFor(r => r.Email)
                .MustAsync(async (email, _) => !await accountService.ExistsEmail(email))
                .WithErrorCode(ValidationCode.Unique.ToString());

            RuleFor(r => r.Username)
                .NotEmpty()
                .WithErrorCode(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Username)
                .MustAsync(async (username, _) => !await accountService.ExistsUsername(username))
                .WithErrorCode(ValidationCode.Unique.ToString());
        }
    }
}
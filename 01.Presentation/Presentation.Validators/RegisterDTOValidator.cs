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

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Email)
                .EmailAddress()
                .WithMessage(ValidationCode.ValidEmailAddress.ToString());
            RuleFor(r => r.Email)
                .MustAsync(async (email, _) => !await accountService.ExistsEmail(email))
                .WithMessage(ValidationCode.Unique.ToString());

            RuleFor(r => r.Username)
                .NotEmpty()
                .WithMessage(ValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Username)
                .MustAsync(async (username, _) => !await accountService.ExistsUsername(username))
                .WithMessage(ValidationCode.Unique.ToString());
        }
    }
}
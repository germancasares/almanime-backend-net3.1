using Domain.DTOs;
using Domain.Enums;
using FluentValidation;
using System.Linq;
using Infrastructure.Helpers;

namespace Presentation.Validators.FluentValidation
{
    public class FansubDTOValidator : AbstractValidator<FansubDTO>
    {
        public FansubDTOValidator()
        {
            RuleFor(f => f.Acronym)
                .NotEmpty()
                .WithMessage(EValidationCode.NotEmpty.ToString());
            RuleFor(f => f.Acronym)
                .MinimumLength(2)
                .WithMessage(EValidationCode.MinimumLength.ToString());
            RuleFor(f => f.Acronym)
                .MaximumLength(5)
                .WithMessage(EValidationCode.MaximumLength.ToString());
            RuleFor(f => f.Acronym)
                .Must(a => a.All(char.IsLetter))
                .WithMessage(EValidationCode.AllLetters.ToString());

            RuleFor(f => f.FullName)
                .NotEmpty()
                .WithMessage(EValidationCode.NotEmpty.ToString());
            RuleFor(f => f.FullName)
                .MinimumLength(6)
                .WithMessage(EValidationCode.MinimumLength.ToString());
            RuleFor(f => f.FullName)
                .MaximumLength(20)
                .WithMessage(EValidationCode.MaximumLength.ToString());

            RuleFor(f => f.MainLanguage)
                .IsInEnum()
                .WithMessage(EValidationCode.IsNotInEnum.ToString());

            RuleFor(f => f.MembershipOption)
                .IsInEnum()
                .WithMessage(EValidationCode.IsNotInEnum.ToString());

            When(f => !string.IsNullOrEmpty(f.Webpage), () =>
            {
                RuleFor(f => f.Webpage)
                    .Must(w => w.IsUri())
                    .WithMessage(EValidationCode.IsNotUri.ToString());
            });
        }
    }
}

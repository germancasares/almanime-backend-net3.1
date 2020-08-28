using Domain.DTOs;
using Domain.Enums;
using FluentValidation;

namespace Presentation.Validators.FluentValidation
{
    public class SubtitleDTOValidator : AbstractValidator<SubtitleDTO>
    {
        public SubtitleDTOValidator()
        {
            RuleFor(r => r.Subtitle)
                .Must(subtitle => subtitle.IsSubtitle())
                .WithMessage(EValidationCode.ContentTypeNotValid.ToString());
        }
    }
}

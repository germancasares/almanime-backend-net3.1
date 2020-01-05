using Domain.DTOs;
using FluentValidation;

namespace Presentation.Validators
{
    public class SubtitleDTOValidator : AbstractValidator<SubtitleDTO>
    {
        public SubtitleDTOValidator()
        {
            RuleFor(r => r.Subtitle)
                .Must(subtitle => subtitle.IsSubtitle())
                .WithMessage(ValidationCode.ContentTypeNotValid.ToString());
        }
    }
}

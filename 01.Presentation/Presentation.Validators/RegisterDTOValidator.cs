using Application.Interfaces;
using Domain.DTOs.Account;
using FluentValidation;
using Infrastructure.Helpers;
using SixLabors.ImageSharp;
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

            When(r => r.Avatar != null, () =>
            {
                RuleFor(r => r.Avatar)
                    .Must(avatar => avatar.IsImage())
                    .WithMessage(ValidationCode.ContentTypeNotValid.ToString());
                RuleFor(r => r.Avatar.Length)
                    .LessThanOrEqualTo(2.MbToBytes())
                    .WithMessage(ValidationCode.MaximumLength.ToString());

                When(r => r.Avatar.IsImage(), () =>
                {
                    RuleFor(r => r.Avatar)
                        .Must(avatar =>
                        {
                            var image = Image.Load(avatar.OpenReadStream());
                            return image.Width == image.Height;
                        })
                        .WithMessage(ValidationCode.ImageAspectRatio.ToString());
                    RuleFor(r => r.Avatar)
                        .Must(avatar =>
                        {
                            var image = Image.Load(avatar.OpenReadStream());
                            return image.Width <= 512 && image.Height <= 512;
                        })
                        .WithMessage(ValidationCode.ImageResolution.ToString());
                });
            });
        }
    }
}
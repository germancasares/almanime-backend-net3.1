using Application.Interfaces;
using Domain.DTOs.Account;
using Domain.Enums;
using FluentValidation;
using Infrastructure.Helpers;
using SixLabors.ImageSharp;
using System.Linq;

namespace Presentation.Validators.FluentValidation
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator(IAccountService accountService)
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

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage(EValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Email)
                .EmailAddress()
                .WithMessage(EValidationCode.ValidEmailAddress.ToString());
            RuleFor(r => r.Email)
                .MustAsync(async (email, _) => !await accountService.ExistsEmail(email))
                .WithMessage(EValidationCode.Unique.ToString());

            RuleFor(r => r.Username)
                .NotEmpty()
                .WithMessage(EValidationCode.NotEmpty.ToString());
            RuleFor(r => r.Username)
                .MustAsync(async (username, _) => !await accountService.ExistsUsername(username))
                .WithMessage(EValidationCode.Unique.ToString());

            When(r => r.Avatar != null, () =>
            {
                RuleFor(r => r.Avatar)
                    .Must(avatar => avatar.IsImage())
                    .WithMessage(EValidationCode.ContentTypeNotValid.ToString());
                RuleFor(r => r.Avatar.Length)
                    .LessThanOrEqualTo(2.MbToBytes())
                    .WithMessage(EValidationCode.MaximumLength.ToString());

                When(r => r.Avatar.IsImage(), () =>
                {
                    RuleFor(r => r.Avatar)
                        .Must(avatar =>
                        {
                            var image = Image.Load(avatar.OpenReadStream());
                            return image.Width == image.Height;
                        })
                        .WithMessage(EValidationCode.ImageAspectRatio.ToString());
                    RuleFor(r => r.Avatar)
                        .Must(avatar =>
                        {
                            var image = Image.Load(avatar.OpenReadStream());
                            return image.Width <= 512 && image.Height <= 512;
                        })
                        .WithMessage(EValidationCode.ImageResolution.ToString());
                });
            });
        }
    }
}
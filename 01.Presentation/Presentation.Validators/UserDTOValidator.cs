using Application.Interfaces;
using Domain.DTOs;
using FluentValidation;
using Infrastructure.Helpers;
using SixLabors.ImageSharp;

namespace Presentation.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator(IUserService userService)
        {
            When(r => r.Name != null, () =>
            {
                RuleFor(r => r.Name)
                    .Must(name => !userService.ExistsName(name))
                    .WithMessage(ValidationCode.Unique.ToString());
            });

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
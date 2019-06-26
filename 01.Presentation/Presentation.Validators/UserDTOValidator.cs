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
            When(r => r.NickName != null, () =>
            {
                RuleFor(r => r.NickName)
                    .Must(nickName => !userService.ExistsNickName(nickName))
                    .WithMessage(ValidationCode.Unique.ToString());
            });

            When(r => r.Avatar != null, () =>
            {
                RuleFor(r => r.Avatar.ContentType)
                    .Must(contentType => IsImage(contentType))
                    .WithMessage(ValidationCode.ContentTypeNotValid.ToString());
                RuleFor(r => r.Avatar.Length)
                    .LessThanOrEqualTo(2.MbToBytes())
                    .WithMessage(ValidationCode.MaximumLength.ToString());

                When(r => IsImage(r.Avatar.ContentType), () =>
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

        private bool IsImage(string contentType) => contentType == "image/png" || contentType == "image/jpg" || contentType == "image/jpeg";
    }
}
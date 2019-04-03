using Domain.DTOs.Account;
using FluentValidation;

namespace Presentation.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(r => r.Identifier).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
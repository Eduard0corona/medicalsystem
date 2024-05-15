using FluentValidation;

namespace MS.Application.Authorization.Features.UserFeatures.Queries.LoginUser
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50).EmailAddress();
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Your password cannot be empty");
        }
    }
}

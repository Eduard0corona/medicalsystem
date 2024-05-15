using FluentValidation;

namespace MS.Application.Authorization.Features.UserFeatures.Commands.CreateUser
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").MaximumLength(255).EmailAddress();
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Your password cannot be empty")
                .MaximumLength(255);
        }
    }
}

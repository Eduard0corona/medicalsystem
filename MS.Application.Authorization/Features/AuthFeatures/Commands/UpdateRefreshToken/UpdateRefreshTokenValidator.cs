using FluentValidation;

namespace MS.Application.Authorization.Features.AuthFeatures.Commands.UpdateRefreshToken
{
    public class UpdateRefreshTokenValidator : AbstractValidator<UpdateRefreshTokenRequest>
    {
        public UpdateRefreshTokenValidator()
        {
            RuleFor(x => x.RefreshToken)
              .NotEmpty()
              .WithMessage("Refresh token is required.")
              .MaximumLength(255);
        }
    }
}

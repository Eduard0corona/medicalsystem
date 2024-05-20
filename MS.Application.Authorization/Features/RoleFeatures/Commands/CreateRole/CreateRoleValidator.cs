using FluentValidation;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(255);
        }
    }
}

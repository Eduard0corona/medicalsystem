using FluentValidation;

namespace MS.Application.Authorization.Features.RoleFeatures.Commands.UpdateRole
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleValidator()
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

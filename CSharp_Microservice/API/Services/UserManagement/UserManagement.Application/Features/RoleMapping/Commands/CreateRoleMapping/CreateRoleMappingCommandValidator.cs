
using FluentValidation;


namespace UserManagement.Application.Features.RoleMapping.Commands.CreateRoleMapping
{
    public class CreateRoleMappingCommandValidator : AbstractValidator<CreateRoleMappingCommand>
    {
       public CreateRoleMappingCommandValidator()
       {
         RuleFor(s => s.AssessmentPeriodTo).GreaterThan(a => a.AssessmentPeriodFrom)
                .WithMessage("To Period must be greater than From Period");
       }

    }
}

using FluentValidation;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup
{
    public class CreateCompetencyGroupCommandValidator : AbstractValidator<CreateCompetencyGroupCommand>
    {
        public CreateCompetencyGroupCommandValidator()
        {
            RuleFor(p => p.CompetencyGroupName)
                .NotEmpty().WithMessage("{Competency Group Name} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Competency Group Name} must not exceed 50 characters.");
        }
    }
}

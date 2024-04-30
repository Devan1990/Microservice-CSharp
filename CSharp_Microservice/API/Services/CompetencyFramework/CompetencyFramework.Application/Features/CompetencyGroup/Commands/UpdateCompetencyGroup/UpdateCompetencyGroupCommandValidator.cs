using FluentValidation;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.UpdateCompetencyGroup
{
    public class UpdateCompetencyGroupCommandValidator : AbstractValidator<UpdateCompetencyGroupCommand>
    {
        public UpdateCompetencyGroupCommandValidator()
        {
            RuleFor(p => p.CompetencyGroupName)
                .NotEmpty().WithMessage("{Competency Group Name} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Competency Group Name} must not exceed 50 characters.");
        }
    }
}

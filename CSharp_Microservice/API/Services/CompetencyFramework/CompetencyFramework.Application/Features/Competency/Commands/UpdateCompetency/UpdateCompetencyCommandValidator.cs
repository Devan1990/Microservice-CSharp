using FluentValidation;

namespace CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency
{
    public class UpdateCompetencyCommandValidator : AbstractValidator<UpdateCompetencyCommand>
    {
        public UpdateCompetencyCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Competency Name} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Competency Name} must not exceed 50 characters.");
        }
    }
}

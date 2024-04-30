using FluentValidation;

namespace CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute
{
    public class UpdateAttributeCommandValidator : AbstractValidator<UpdateAttributeCommand>
    {
        public UpdateAttributeCommandValidator()
        {
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{Attribute Name} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Attribute Name} must not exceed 50 characters.");
        }
    }
}

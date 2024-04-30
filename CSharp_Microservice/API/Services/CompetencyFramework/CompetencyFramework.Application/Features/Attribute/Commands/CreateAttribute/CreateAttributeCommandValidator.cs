using FluentValidation;

namespace CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute
{
    public class CreateAttributeCommandValidator : AbstractValidator<CreateAttributeCommand>
    {
        public CreateAttributeCommandValidator()
        {
            //RuleFor(p => p.Description)
            //    .NotEmpty()
            //    .WithMessage("{Attribute Description} is required.")
            //    .NotNull()
            //    .MaximumLength(50)
            //    .WithMessage("{Attribute Description} must not exceed 50 characters.");
        }
    }
}

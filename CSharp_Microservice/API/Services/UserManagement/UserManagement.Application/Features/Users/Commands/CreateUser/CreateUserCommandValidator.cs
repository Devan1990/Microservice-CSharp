using FluentValidation;
using UserManagement.Application.Features.Users.Commands.CreateUser;

namespace UserManagement.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //RuleFor(p => p.UserName)
            //    .NotEmpty().WithMessage("{UserName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            //RuleFor(p => p.EmailAddress)
            //   .NotEmpty().WithMessage("{EmailAddress} is required.");

            //RuleFor(p => p.TotalPrice)
            //    .NotEmpty().WithMessage("{TotalPrice} is required.")
            //    .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");

           
                RuleFor(p => p.FirstName)
                    .NotEmpty().WithMessage("{User Name is required.")
                    .NotNull()
                    .MaximumLength(50).WithMessage("{User Name} must not exceed 50 characters.");
            
        }
    }
}

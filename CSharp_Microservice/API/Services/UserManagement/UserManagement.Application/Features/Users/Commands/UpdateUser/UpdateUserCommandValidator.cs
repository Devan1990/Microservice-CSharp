using FluentValidation;

namespace UserManagement.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{FirstName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{FirstName} must not exceed 50 characters.");
        }
    }
}

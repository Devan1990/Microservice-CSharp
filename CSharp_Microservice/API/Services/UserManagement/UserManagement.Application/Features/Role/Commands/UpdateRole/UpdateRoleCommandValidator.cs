using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(p => p.RoleName)
                .NotEmpty().WithMessage("{Role Name} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{Role Name} must not exceed 50 characters.");
        }
    }
}

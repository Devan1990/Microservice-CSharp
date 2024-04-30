using MediatR;
using System.Collections.Generic;

namespace UserManagement.Application.Features.Role.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<long>
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public long RoleTypeId { get; set; }
        public ICollection<CreateRoleCompetenciesMapVm> CompetenciesMap { get; set; }

    }
}

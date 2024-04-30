using MediatR;
using System.Collections.Generic;
using UserManagement.Application.Features.Role.Commands.CreateRole;

namespace UserManagement.Application.Features.Role.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public long RoleTypeId { get; set; }
        public ICollection<UpdateRoleCompetenciesMapVm> CompetenciesMap { get; set; }
    }
}

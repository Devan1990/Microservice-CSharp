using System;
using System.Collections.Generic;
using UserManagement.Application.Features.Role.Commands.CreateRole;

namespace UserManagement.Application.Features.Role.Queries.GetRoles
{
    public class RoleVm2
    {
        public long Id { get; set; }
        public Guid? RoleGuid { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public RoleTypeVm RoleType { get; set; }

        public ICollection<GetRoleCompetenciesMapVm> CompetenciesMap { get; set; }
    }
}

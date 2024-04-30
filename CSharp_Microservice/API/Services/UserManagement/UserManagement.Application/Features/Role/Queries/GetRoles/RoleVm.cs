using CompetencyFramework.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Features.Role.Commands.CreateRole;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Features.Role.Queries.GetRoles
{
    public class RoleVm
    {
        public long Id { get; set; }
        public Guid? RoleGuid { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public RoleTypeVm RoleType { get; set; }
        public GetCompetencyDetailsResponse CompetenciesMap { get; set; }
    }
}


using UserManagement.Application.Features.Role.Queries.GetRoles;

namespace CAT.BFF.Models
{
    public class RoleVm2
    {
        public int Id { get; set; }
        public Guid? RoleGuid { get; set; }
        public string RoleId { get; set; }
       // public string RoleAId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public RoleTypeVm RoleType { get; set; }
        public ICollection<CreateRoleCompetenciesMapVm> CompetenciesMap { get; set; }
    }
}

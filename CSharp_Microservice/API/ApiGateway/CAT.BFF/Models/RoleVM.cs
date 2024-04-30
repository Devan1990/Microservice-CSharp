namespace CAT.BFF.Models
{
    public class RoleVM
    {
        public int Id { get; set; }
        public Guid? RoleGuid { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public RoleTypeVm RoleType { get; set; }
        public List<CompetencyGroupsRolesVm> CompetenciesMap { get; set; }
    }
}

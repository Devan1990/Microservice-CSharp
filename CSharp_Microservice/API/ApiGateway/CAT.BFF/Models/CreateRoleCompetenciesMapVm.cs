namespace CAT.BFF.Models
{
    public class CreateRoleCompetenciesMapVm
    {
        public long Id { get; set; }
        public long CompetencyGroupId { get; set; }
        public long CompetencyId { get; set; }
        public bool IsSelected { get; set; } = false;
        public long CompetencyLevelId { get; set; }
        public long ExpectedLevelId { get; set; }
    }
}

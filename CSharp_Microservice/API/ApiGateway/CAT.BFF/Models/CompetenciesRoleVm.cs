using CATBFF.Models;

namespace CAT.BFF.Models
{
    public class CompetenciesRoleVm
    {
         public long CompetencyMapId { get; set; }
        public int Id { get; set; }
        public string CompetencyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ExpectedLevelId { get; set; }
        public string ExpectedLevelName { get; set; }
        public bool IsSelected { get; set; }
        public ICollection<AttributesVm> Attributes { get; set; }
    }
}

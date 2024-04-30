using CompetencyFramework.Domain.Common;

namespace CompetencyFramework.Domain.Entities
{
    public class Attribute : EntityBase
    {
        public string Description { get; set; }
        public CompetencyLevel CompetencyLevel { get; set; }
        public Competency Competency { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

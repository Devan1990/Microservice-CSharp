using CompetencyFramework.Domain.Common;
using System.Collections.Generic;

namespace CompetencyFramework.Domain.Entities
{
    public class Competency : EntityBase
    {
        public string CompetencyId { get; set; } = null;
        public string Name { get; set; }
        public string Description { get; set; }
        public CompetencyGroup CompetencyGroup { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

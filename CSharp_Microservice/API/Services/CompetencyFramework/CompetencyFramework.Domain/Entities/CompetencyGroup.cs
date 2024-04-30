using CompetencyFramework.Domain.Common;
using System;
using System.Collections.Generic;

namespace CompetencyFramework.Domain.Entities
{
    public class CompetencyGroup : EntityBase
    {
        public string CompetencyGroupId { get; set; } = null;
        public string Name { get; set; }
        public string Description { get; set; }
        public ActiveStatus Status { get; set; }
        public virtual ICollection<Competency> Competencies { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

using CompetencyFramework.Domain.Common;
using System.Collections.Generic;

namespace CompetencyFramework.Domain.Entities
{
    public class CompetencyLevel : EntityBase
    {
        public string Name { get; set; }
        public int Weightage { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}

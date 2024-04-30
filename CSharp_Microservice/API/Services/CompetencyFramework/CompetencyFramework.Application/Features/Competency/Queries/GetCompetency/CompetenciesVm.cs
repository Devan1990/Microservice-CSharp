using CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.Competency.Queries.GetCompetency
{
    public class CompetenciesVm
    {
        public int Id { get; set; }
        public string CompetencyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<AttributesVm> Attributes { get; set; }
    }
}

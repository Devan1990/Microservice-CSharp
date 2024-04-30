using CompetencyFramework.Application.Features.Competency.Queries.GetCompetency;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup
{
    public class CompetencyGroupsVm
    {
        public int Id { get; set; }
        public string CompetencyGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public ICollection<CompetenciesVm> Competencies { get; set; }
    }
}

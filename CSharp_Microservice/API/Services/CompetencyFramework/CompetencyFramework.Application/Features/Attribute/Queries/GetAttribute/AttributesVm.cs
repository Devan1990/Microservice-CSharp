using CompetencyFramework.Application.Features.Competency.Queries.GetCompetency;
using CompetencyFramework.Domain.Entities;

namespace CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute
{
    public interface AttributesVm
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CompetencyId { get; set; }
        public CompetencyLevelVm CompetencyLevel { get; set; }
    }
}

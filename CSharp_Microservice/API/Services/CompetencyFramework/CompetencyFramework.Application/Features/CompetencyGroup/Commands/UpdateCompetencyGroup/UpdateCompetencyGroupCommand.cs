using CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency;
using MediatR;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.UpdateCompetencyGroup
{
    public class UpdateCompetencyGroupCommand : IRequest<long>
    {
        public long Id { get; set; }
        public string CompetencyGroupName { get; set; }
        public string CompetencyGroupDescription { get; set; }

        public List<UpdateCGCompetencyCommand> Competencies { get; set; }
    }
}

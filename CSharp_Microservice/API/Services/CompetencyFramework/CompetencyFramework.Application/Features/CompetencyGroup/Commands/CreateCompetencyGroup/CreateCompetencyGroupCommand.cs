using CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.CreateCompetencyGroup
{
    public class CreateCompetencyGroupCommand : IRequest<long>
    { 
        public string CompetencyGroupName { get; set; }
        public string CompetencyGroupDescription { get; set; }

        public List<CreateCGCompetencyCommand> Competencies { get; set; }
    }
}

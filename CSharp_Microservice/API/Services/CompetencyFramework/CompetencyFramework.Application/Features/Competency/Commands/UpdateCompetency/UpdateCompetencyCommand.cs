using CompetencyFramework.Application.Features.Attribute.Commands.DeleteAttribute;
using CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute;
using MediatR;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency
{
    public class UpdateCompetencyCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UpdateAttributeCommand> Attributes { get; set; }
    }
}

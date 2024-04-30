using CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute;
using MediatR;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency
{
    public class CreateCompetencyCommand : IRequest<long>
    {
        public long CompetencyGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CreateAttributeCommand> Attributes { get; set; }
    }
}

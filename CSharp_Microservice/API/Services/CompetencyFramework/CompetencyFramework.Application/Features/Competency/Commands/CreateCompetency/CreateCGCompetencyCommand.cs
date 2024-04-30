using System.Collections.Generic;
using CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute;
namespace CompetencyFramework.Application.Features.Competency.Commands.CreateCompetency
{
    public class CreateCGCompetencyCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CreateCompentencyGroupAttributeCommand> Attributes { get; set; }
    }
}

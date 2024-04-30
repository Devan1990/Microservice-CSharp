using System.Collections.Generic;
using CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute;
namespace CompetencyFramework.Application.Features.Competency.Commands.UpdateCompetency
{
    public class UpdateCGCompetencyCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UpdateCGAttributeCommand> Attributes { get; set; }
    }
}

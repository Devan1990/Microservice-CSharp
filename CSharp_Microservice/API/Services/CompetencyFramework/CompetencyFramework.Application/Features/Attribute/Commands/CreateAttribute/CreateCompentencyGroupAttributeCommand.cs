using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute
{
    public class CreateCompentencyGroupAttributeCommand
    {
        public long CompetencyLevelId { get; set; }
        public string Description { get; set; }
    }
}

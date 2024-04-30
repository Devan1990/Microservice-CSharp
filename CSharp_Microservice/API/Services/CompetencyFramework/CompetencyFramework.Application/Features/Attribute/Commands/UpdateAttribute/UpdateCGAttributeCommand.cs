using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute
{
    public class UpdateCGAttributeCommand
    {
        public long Id { get; set; }
        public long CompetencyLevelId { get; set; }
        public string Description { get; set; }
    }
}

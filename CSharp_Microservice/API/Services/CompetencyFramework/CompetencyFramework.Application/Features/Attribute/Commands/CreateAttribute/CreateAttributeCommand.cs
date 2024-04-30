using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Features.Attribute.Commands.CreateAttribute
{
    public class CreateAttributeCommand : IRequest<long>
    {
        public long CompetencyId { get; set; }
        public long CompetencyLevelId { get; set; }
        public string Description { get; set; }
    }
}

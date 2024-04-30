using MediatR;
using System.Collections.Generic;

namespace CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute
{
    public class GetAttributeListQuery : IRequest<List<AttributesVm>>
    {
        public GetAttributeListQuery()
        {
        }
    }
}

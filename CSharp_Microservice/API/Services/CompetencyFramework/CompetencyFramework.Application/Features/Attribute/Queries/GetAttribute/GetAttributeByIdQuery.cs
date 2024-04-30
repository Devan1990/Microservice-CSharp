using MediatR;

namespace CompetencyFramework.Application.Features.Attribute.Queries.GetAttribute
{
    public class GetAttributeByIdQuery : IRequest<AttributesVm>
    {
        public long Id { get; set; }

        public GetAttributeByIdQuery(long id)
        {
            Id = id;
        }
    }
}

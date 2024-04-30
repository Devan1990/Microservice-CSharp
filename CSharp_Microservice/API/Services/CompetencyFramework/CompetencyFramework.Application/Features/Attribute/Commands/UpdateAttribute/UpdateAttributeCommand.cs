using MediatR;

namespace CompetencyFramework.Application.Features.Attribute.Commands.UpdateAttribute
{
    public class UpdateAttributeCommand : IRequest
    {
        public long Id { get; set; }
        public long CompetencyLevelId { get; set; }
        public string Description { get; set; }
    }
}

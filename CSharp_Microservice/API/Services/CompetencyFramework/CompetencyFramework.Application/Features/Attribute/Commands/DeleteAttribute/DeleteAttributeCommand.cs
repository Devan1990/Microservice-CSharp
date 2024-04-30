using MediatR;

namespace CompetencyFramework.Application.Features.Attribute.Commands.DeleteAttribute
{
    public class DeleteAttributeCommand : IRequest
    {
        public int Id { get; set; }
    }
}

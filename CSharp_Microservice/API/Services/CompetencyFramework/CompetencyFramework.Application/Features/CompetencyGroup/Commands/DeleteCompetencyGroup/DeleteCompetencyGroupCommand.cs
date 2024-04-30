using MediatR;

namespace CompetencyFramework.Application.Features.CompetencyGroup.Commands.DeleteCompetencyGroup
{
    public class DeleteCompetencyGroupCommand : IRequest
    {
        public int Id { get; set; }
    }
}

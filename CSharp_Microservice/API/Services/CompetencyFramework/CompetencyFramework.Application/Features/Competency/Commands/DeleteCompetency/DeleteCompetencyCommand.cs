using MediatR;

namespace CompetencyFramework.Application.Features.Competency.Commands.DeleteCompetency
{
    public class DeleteCompetencyCommand : IRequest
    {
        public int Id { get; set; }
    }
}

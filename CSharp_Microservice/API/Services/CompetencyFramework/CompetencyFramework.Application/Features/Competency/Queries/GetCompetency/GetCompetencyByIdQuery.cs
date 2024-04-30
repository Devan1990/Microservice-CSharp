using MediatR;

namespace CompetencyFramework.Application.Features.Competency.Queries.GetCompetency
{
    public class GetCompetencyByIdQuery : IRequest<CompetenciesVm>
    {
        public long Id { get; set; }

        public GetCompetencyByIdQuery(long id)
        {
            Id = id;
        }
    }
}

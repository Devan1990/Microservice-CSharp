using CompetencyFramework.Application.Contracts.Persistence;

namespace CompetencyFramework.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ICompetencyGroupRepository competencyGroupRepository
            , ICompetencyRepository competencyRepository, IAttributeRepository attributeRepository, ICompetencyLevelRepository competencyLevelRepository)
        {
            CompetencyGroupRepository = competencyGroupRepository;
            CompetencyRepository = competencyRepository;
            AttributeRepository = attributeRepository;
            CompetencyLevelRepository = competencyLevelRepository;
        }

        public ICompetencyGroupRepository CompetencyGroupRepository { get; }
        public ICompetencyRepository CompetencyRepository { get; }
        public IAttributeRepository AttributeRepository { get; }
        public ICompetencyLevelRepository CompetencyLevelRepository { get; }
    }
}

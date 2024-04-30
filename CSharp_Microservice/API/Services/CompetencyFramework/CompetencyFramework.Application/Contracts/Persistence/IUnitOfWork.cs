namespace CompetencyFramework.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        ICompetencyGroupRepository CompetencyGroupRepository { get; }
        ICompetencyRepository CompetencyRepository { get; }
        IAttributeRepository AttributeRepository { get; }
        ICompetencyLevelRepository CompetencyLevelRepository { get; }
    }
}
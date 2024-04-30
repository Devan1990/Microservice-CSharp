using CompetencyFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Contracts.Persistence
{
    public interface ICompetencyGroupRepository : IAsyncRepository<CompetencyGroup>
    {
        Task<long> AddCompetencyGroup(CompetencyGroup group);
        Task<CompetencyGroup> GetCompetencyGroupById(long id);
        Task<IEnumerable<CompetencyGroup>> GetCompetencyGroupListById(List<long> ids);
        Task<IEnumerable<CompetencyGroup>> GetCompetencyGroups();
        Task<CompetencyGroup> GetCompetencyGroupOrderById(Expression<Func<CompetencyGroup, bool>> predicate = null, Func<IQueryable<CompetencyGroup>, IOrderedQueryable<CompetencyGroup>> orderBy = null, string includeString = null, bool disableTracking = true);
        Task<long> UpdateCompetencyGroup(CompetencyGroup group);

    }
}

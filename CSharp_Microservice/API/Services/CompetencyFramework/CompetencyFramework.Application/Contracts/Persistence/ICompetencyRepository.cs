using CompetencyFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Contracts.Persistence
{
    public interface ICompetencyRepository : IAsyncRepository<Competency>
    {
        Task<long> AddCompetency(Competency group);
        Task<Competency> GetCompetencyById(long id);
        Task<IEnumerable<Competency>> GetCompetencyListById(List<long> ids);
        Task<IEnumerable<Competency>> GetCompetencies();
        Task<Competency> GetCompetencyOrderById(Expression<Func<Competency, bool>> predicate = null, Func<IQueryable<Competency>, IOrderedQueryable<Competency>> orderBy = null, string includeString = null, bool disableTracking = true);
    }
}

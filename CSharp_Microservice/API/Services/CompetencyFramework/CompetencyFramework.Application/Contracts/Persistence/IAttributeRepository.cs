using CompetencyFramework.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Contracts.Persistence
{
    public interface IAttributeRepository : IAsyncRepository<Attribute>
    {
        Task<long> AddAttribute(Attribute attribute);
        Task<Attribute> GetAttributeById(long id);
        Task<IEnumerable<Attribute>> GetAttributes();
        Task<Attribute> GetAttributeOrderById(Expression<System.Func<Attribute, bool>> predicate = null, System.Func<IQueryable<Attribute>, IOrderedQueryable<Attribute>> orderBy = null, string includeString = null, bool disableTracking = true);
        void DisposeAttribute(Attribute attribute);
    }
}

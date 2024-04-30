using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Domain.Entities;
using CompetencyFramework.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompetencyFramework.Infrastructure.Repositories
{
    public class AttributeRepository : RepositoryBase<Attribute>, IAttributeRepository
    {
        public AttributeRepository(CompetencyFrameworkContext dbContext) : base(dbContext)
        {
        }

        public async Task<long> AddAttribute(Attribute attribute)
        {
            var ret = await _dbContext.AddAsync(attribute);
            return ret.Entity.Id;
        }

        public async Task<IEnumerable<Attribute>> GetAttributes()
        {
            var attributeList = await _dbContext.Attributes
                                      .Include(a => a.CompetencyLevel)
                                      .Include(a => a.Competency)
                                      .ToListAsync();

            return attributeList;
        }

        public async Task<Attribute> GetAttributeById(long id)
        {
            var attribute = await _dbContext.Attributes
                                  .Include(a => a.CompetencyLevel)
                                  .Include(a => a.Competency)
                                  .FirstOrDefaultAsync(a => a.Id == id);

            return attribute;
        }

        public async Task<Attribute> GetAttributeOrderById(Expression<System.Func<Attribute, bool>> predicate = null, System.Func<IQueryable<Attribute>, IOrderedQueryable<Attribute>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.Attributes.Any())
                {
                    return await (_dbContext.Attributes).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.Attributes).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.Attributes).FirstOrDefaultAsync();
        }

        public void DisposeAttribute(Attribute attribute)
        {
            _dbContext.Entry(attribute).State = EntityState.Detached;
        }

    }
}

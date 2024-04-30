using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Domain.Entities;
using CompetencyFramework.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompetencyFramework.Infrastructure.Repositories
{
    public class CompetencyRepository : RepositoryBase<Competency>, ICompetencyRepository
    {
        public CompetencyRepository(CompetencyFrameworkContext dbContext) : base(dbContext)
        {
        }

        public async Task<long> AddCompetency(Competency competency)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Competencies.Add(competency);
                    var cg = await _dbContext.SaveChangesAsync();
                    competency.CompetencyId = "CM" + competency.Id.ToString().PadLeft(competency.Id.ToString().Length + 5 - competency.Id.ToString().Length, '0');
                    var cm = await _dbContext.SaveChangesAsync();
                    transaction.Commit();

                    return competency.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }

            //var ret = await _dbContext.AddAsync(competency);
            //return ret.Entity.Id;
        }

        public async Task<IEnumerable<Competency>> GetCompetencies()
        {
            var competencyList = await _dbContext.Competencies
                .Include("Attributes.CompetencyLevel")
                .ToListAsync();

            return competencyList;
        }

        public async Task<Competency> GetCompetencyById(long id)
        {
            var competency = await _dbContext.Competencies
                                   .Include(a => a.Attributes)
                                   .Include(a => a.CompetencyGroup)
                                   .FirstOrDefaultAsync(a => a.Id == id);

            return competency;
        }

        public async Task<IEnumerable<Competency>> GetCompetencyListById(List<long> ids)
        {
            var competencies = await _dbContext.Competencies
                                  .Include(a => a.Attributes)
                                  .Include(a => a.CompetencyGroup)
                                  .Where(w => ids.Contains(w.Id))
                                  .ToListAsync();

            return competencies;
        }

        public async Task<Competency> GetCompetencyOrderById(Expression<Func<Competency, bool>> predicate = null, Func<IQueryable<Competency>, IOrderedQueryable<Competency>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.Competencies.Any())
                {
                    return await (_dbContext.Competencies).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.Competencies).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.Competencies).FirstOrDefaultAsync();
        }
    }
}

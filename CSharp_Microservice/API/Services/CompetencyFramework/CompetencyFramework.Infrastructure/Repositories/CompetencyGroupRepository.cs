using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Exceptions;
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
    public class CompetencyGroupRepository : RepositoryBase<CompetencyGroup>, ICompetencyGroupRepository
    {
        public CompetencyGroupRepository(CompetencyFrameworkContext dbContext) : base(dbContext)
        {
        }

        public async Task<long> AddCompetencyGroup(CompetencyGroup group)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.CompetencyGroups.Add(group);
                    var cg = await _dbContext.SaveChangesAsync();
                    group.CompetencyGroupId = "CG" + group.Id.ToString().PadLeft(group.Id.ToString().Length + 5 - group.Id.ToString().Length, '0');
                    foreach (var c in group.Competencies)
                    {
                        c.CompetencyId = "CM" + c.Id.ToString().PadLeft(c.Id.ToString().Length + 5 - c.Id.ToString().Length, '0');
                        
                    }
                    var cm = await _dbContext.SaveChangesAsync();
                    transaction.Commit();

                    return group.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<CompetencyGroup> GetCompetencyGroupById(long id)
        {
            var competencyGroup = await _dbContext.CompetencyGroups
                                             .Include(x => x.Competencies.Where(x => x.IsDeleted == false))
                                            .ThenInclude(x => x.Attributes.Where(x => x.IsDeleted == false))
                                            .ThenInclude(x => x.CompetencyLevel)
                                        //.Include("Competencies.Attributes.CompetencyLevel")
                                        .FirstOrDefaultAsync(a => a.Id == id);
            return competencyGroup;
        }
        public async Task<IEnumerable<CompetencyGroup>> GetCompetencyGroupListById(List<long> ids)
        {
            var competencies = await _dbContext.CompetencyGroups
                                  .Include(a => a.Competencies.Where(x => x.IsDeleted == false))
                                  .ThenInclude(a => a.Attributes.Where(x => x.IsDeleted == false))
                                  .ThenInclude(a => a.CompetencyLevel)
                                  .Where(w => ids.Contains(w.Id))
                                  .ToListAsync();

            return competencies;
        }

        public async Task<IEnumerable<CompetencyGroup>> GetCompetencyGroups()
        {
            var competencyGroupList = await _dbContext.CompetencyGroups
                                            .Include(x => x.Competencies.Where(x => x.IsDeleted == false))
                                            .ThenInclude(x => x.Attributes.Where(x => x.IsDeleted == false))
                                            .ThenInclude(x => x.CompetencyLevel)
                                            //.Include("Competencies.Attributes.CompetencyLevel")
                                            .ToListAsync();
            return competencyGroupList;
        }

        public async Task<CompetencyGroup> GetCompetencyGroupOrderById(Expression<Func<CompetencyGroup, bool>> predicate = null, Func<IQueryable<CompetencyGroup>, IOrderedQueryable<CompetencyGroup>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.CompetencyGroups.Any())
                {
                    return await (_dbContext.CompetencyGroups).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.CompetencyGroups).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.CompetencyGroups).FirstOrDefaultAsync();
        }

        public async Task<long> UpdateCompetencyGroup(CompetencyGroup group)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                  
                    _dbContext.CompetencyGroups.Add(group);
                    _dbContext.Entry(group).State = EntityState.Modified;
                    var cg = await _dbContext.SaveChangesAsync();
                    foreach (var c in group.Competencies)
                    {
                        c.CompetencyId = "CM" + c.Id.ToString().PadLeft(c.Id.ToString().Length + 5 - c.Id.ToString().Length, '0');

                    }
                    var cm = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return group.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

    }
}
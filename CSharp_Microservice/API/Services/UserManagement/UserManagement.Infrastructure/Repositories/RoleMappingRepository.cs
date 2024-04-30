
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;


namespace UserManagement.Infrastructure.Repositories
{
    public class RoleMappingRepository : RepositoryBase<RoleMapping>, IRoleMappingRepository
    {
        public RoleMappingRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }
        public async Task<long> AddRoleMapping(RoleMapping roleMapping)
        {
            var ret = await _dbContext.AddAsync(roleMapping);
            return ret.Entity.Id;
        }
        public async Task<IEnumerable<RoleMapping>> GetRoleMappings()
        {
            var roleMappings = await _dbContext.RoleMappings
                                          .Include(s => s.AssessorRole.Where(x => x.IsDeleted == false))
                                          .ToListAsync();
            return roleMappings;
        }

        public async Task<RoleMapping> GetRoleMappingById(long id)
        {
            var rolemapping = await _dbContext.RoleMappings.Include(s => s.AssessorRole.Where(x => x.IsDeleted == false))
                                        .FirstOrDefaultAsync(a => a.Id == id);
            return rolemapping;
        }

        public async Task<IEnumerable<RoleMapping>> IsExistRoleMappingFY(long RoleId, DateTime AssessmentPeriodFrom, DateTime AssessmentPeriodTo)
        {
            var rolemapping = await _dbContext.RoleMappings.Include(s => s.AssessorRole.Where(x => x.IsDeleted == false))
                                        .Where(a => a.RoleId == RoleId && a.AssessmentPeriodFrom.Year == AssessmentPeriodFrom.Year && a.AssessmentPeriodTo.Year == AssessmentPeriodTo.Year).ToListAsync();
            return rolemapping;
        }
        public async Task<IEnumerable<AssessorRole>> GetAssessrs()
        {
            var assessorRoles = await _dbContext.AssessorRoles

                                          .ToListAsync();
            return assessorRoles;
        }

        public async Task<IReadOnlyList<AssessorRole>> GetRoleMappingQuery(Expression<Func<AssessorRole, bool>> predicate = null, Func<IQueryable<AssessorRole>, IOrderedQueryable<AssessorRole>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<AssessorRole> query = _dbContext.Set<AssessorRole>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
        public async Task<AssessorRole> GetAssessrById(long id)
        {
            var assessorRole = await _dbContext.AssessorRoles
                                        .FirstOrDefaultAsync(a => a.Id == id);
            return assessorRole;

        }
        public async Task<long> UpdateRoleMapping(RoleMapping role)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.RoleMappings.Add(role);
                    _dbContext.Entry(role).State = EntityState.Modified;
                    var cg = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return role.Id;
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

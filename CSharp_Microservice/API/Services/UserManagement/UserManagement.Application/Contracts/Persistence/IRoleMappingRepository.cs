using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace UserManagement.Application.Contracts.Persistence
{
    public interface IRoleMappingRepository : IAsyncRepository<RoleMapping>
    {
        Task<long> AddRoleMapping(RoleMapping roleMapping);
        Task<IEnumerable<RoleMapping>> GetRoleMappings();
        Task<RoleMapping> GetRoleMappingById(long id);
        Task<IEnumerable<AssessorRole>> GetAssessrs();
        Task<AssessorRole> GetAssessrById(long id);
        Task<IEnumerable<RoleMapping>> IsExistRoleMappingFY(long RoleId, DateTime AssessmentPeriodFrom, DateTime AssessmentPeriodTo);
        Task<IReadOnlyList<AssessorRole>> GetRoleMappingQuery(Expression<Func<AssessorRole, bool>> predicate = null, Func<IQueryable<AssessorRole>, IOrderedQueryable<AssessorRole>> orderBy = null, string includeString = null, bool disableTracking = true);
        Task<long> UpdateRoleMapping(RoleMapping roleMapping);
    }
}

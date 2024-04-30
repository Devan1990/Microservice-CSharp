using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistence
{
    public interface IRoleRepository : IAsyncRepository<Role>
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<long> AddRole(Role role);
        Task<Role> GetRoleById(long id);
        Task<RoleType> GetRoleTypeById(long id);
        Task<IEnumerable<RoleType>> GetRoleTypes();
        Task<IEnumerable<ExpectedLevel>> GetExpectedLevels();
        Task<ExpectedLevel> GetExpectedLevelById(long id);
        Task<Role> CheckRole(long roleId);
        Task<Role> GetRoleOrderById(Expression<Func<Role, bool>> predicate = null, Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null, string includeString = null, bool disableTracking = true);
        
        Task<ExpectedLevel> CheckExpectedLevel(long ExpectedLevelId);
        Task<long> UpdateRole(Role role);
    }
}

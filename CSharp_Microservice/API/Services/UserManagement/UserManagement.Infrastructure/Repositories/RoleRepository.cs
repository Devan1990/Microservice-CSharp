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
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Role>> GetRoles()
        
        {
            //var RoleList = await _dbContext.Roles.ToListAsync();
            //return RoleList;
            var RoleList = await _dbContext.Roles
                                          .Include(s => s.CompetenciesMap.Where(x => x.IsDeleted == false)).Include(x => x.RoleType).Where(a => a.IsDeleted == false)
                                          .ToListAsync();
            return RoleList;
        }
        public async Task<long> AddRole(Role role)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Roles.Add(role);
                    var roleres = await _dbContext.SaveChangesAsync();
                    role.RoleId = "RI" + role.Id.ToString().PadLeft(role.Id.ToString().Length + 5 - role.Id.ToString().Length, '0');
                    var rm = await _dbContext.SaveChangesAsync();
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

        public async Task<Role> GetRoleById(long id)
        {
            var role = await _dbContext.Roles
                                       .Include(x => x.CompetenciesMap.Where(x => x.IsDeleted == false)).Include(y => y.RoleType).
                                       Where(a => a.IsDeleted == false)
                                       .FirstOrDefaultAsync(a => a.Id == id);
            return role;

        }
        //CheckRole
        public async Task<Role> CheckRole(long roleId)
        {
            var RoleTypeList = await _dbContext.Roles.
                                Where(x => x.Id == roleId).FirstOrDefaultAsync();
            return RoleTypeList;
        }

        //  public async Task<Role> GetRoleByRoleId(string roleId)
        // {
        //     var RoleTypeList = await _dbContext.Roles.
        //                         Where(x=>x.RoleId==roleId).FirstOrDefaultAsync();
        //     return RoleTypeList;
        // }

        public async Task<IEnumerable<RoleType>> GetRoleTypes()
        {

            var RoleTypeList = await _dbContext.RoleType
                            .ToListAsync();
            return RoleTypeList;
        }
        public async Task<RoleType> GetRoleTypeById(long id)
        {
            var roleTypes = await _dbContext.RoleType
                                .FirstOrDefaultAsync(a => a.Id == id);
            return roleTypes;

        }

        public async Task<IEnumerable<ExpectedLevel>> GetExpectedLevels()
        {

            var expectedLevels = await _dbContext.ExpectedLevels
                            .ToListAsync();
            return expectedLevels;
        }
        public async Task<ExpectedLevel> GetExpectedLevelById(long id)
        {
            var expectedLevel = await _dbContext.ExpectedLevels
                                .FirstOrDefaultAsync(a => a.Id == id);
            return expectedLevel;

        }
        public async Task<ExpectedLevel> CheckExpectedLevel(long ExpectedLevelId)
        {
            var expectedLevel = await _dbContext.ExpectedLevels.
                                Where(x => x.Id == ExpectedLevelId).FirstOrDefaultAsync();
            return expectedLevel;
        }
        public async Task<Role> GetRoleOrderById(Expression<Func<Role, bool>> predicate = null, Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.Roles.Any())
                {
                    return await (_dbContext.Roles).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.Roles).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.Roles).FirstOrDefaultAsync();
        }
        public async Task<long> UpdateRole(Role role)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Roles.Add(role);
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

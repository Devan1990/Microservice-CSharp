
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
    public class VerticalRepository : RepositoryBase<Verticals>, IverticalRepository
    {
        public VerticalRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Verticals>> GetVerticals()
        {
            //var RoleList = await _dbContext.Roles.ToListAsync();
            //return RoleList;
            var VerticalList = await _dbContext.Verticals
                                          //.Include(s => s.Vertical)
                                          .ToListAsync();
            return VerticalList;
        }
        //public async Task<long> AddRole(Role role)
        //{
        //    var ret = await _dbContext.AddAsync(role);
        //    return ret.Entity.Id;
        //}

        public async Task<Verticals> GetVerticalById(int id)
        {
            var verticals = await _dbContext.Verticals
                                .FirstOrDefaultAsync(a => a.Id == id);
            return verticals;

        }


        //public async Task<Role> GetRoleOrderById(Expression<Func<Role, bool>> predicate = null, Func<IQueryable<Role>, IOrderedQueryable<Role>> orderBy = null, string includeString = null, bool disableTracking = true)
        //{
        //    if (orderBy != null)
        //    {
        //        if (!_dbContext.Roles.Any())
        //        {
        //            return await (_dbContext.Roles).FirstOrDefaultAsync();
        //        }
        //        else
        //        {
        //            return await orderBy(_dbContext.Roles).FirstOrDefaultAsync();
        //        }
        //    }
        //    return await (_dbContext.Roles).FirstOrDefaultAsync();
        //}

    }
}

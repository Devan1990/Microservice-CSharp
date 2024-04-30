using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserManagementRepository : RepositoryBase<User>, IUserRepository
    {
        public UserManagementRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }

        //public async Task<IEnumerable<User>> GetOrdersByUserName(int id)
        //{
        //    var userList = await _dbContext.Users
        //                        .Where(o => o.Id == id)
        //                        .ToListAsync();
        //    return userList;
        //}

        public async Task<long> AddUser(User user)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    //var ret = await _dbContext.AddAsync(user);
                    //return ret.Entity.Id;

                    _dbContext.Users.Add(user);
                    var userres = await _dbContext.SaveChangesAsync();
                    user.UserId = "UI" + user.Id.ToString().PadLeft(user.Id.ToString().Length + 5 - user.Id.ToString().Length, '0');
                    var rm = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return user.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return 0;

                }
            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var userList = await _dbContext.Users.Include(a=>a.Role.CompetenciesMap.Where(x => x.IsDeleted == false)).Include(a=>a.Role.RoleType)
                .Include(a=>a.Vertical)
                .Include(a=>a.Country)
                .Include(a=>a.Area)
                .Include(a=>a.Region)
                .ToListAsync();
            return userList;
        }

        public async Task<User> GetUserById(long id)
        {
            var user = await _dbContext.Users.Include(a => a.Role.CompetenciesMap.Where(x => x.IsDeleted == false)).Include(a => a.Role.RoleType)
                .Include(a => a.Vertical)
                .Include(a => a.Country)
                .Include(a => a.Area)
                .Include(a => a.Region)
                                .FirstOrDefaultAsync(a => a.Id == id);
            return user;
        }

        public async Task<User> GetUserOrderById(Expression<Func<User, bool>> predicate = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            if (orderBy != null)
            {
                if (!_dbContext.Users.Any())
                {
                    return await (_dbContext.Users).FirstOrDefaultAsync();
                }
                else
                {
                    return await orderBy(_dbContext.Users).FirstOrDefaultAsync();
                }
            }
            return await (_dbContext.Users).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUserByRoleId(long id)
        {
            try
            {
                var user = await _dbContext.Users.Include(a => a.Role)
                                .Where(o => o.Role.Id == id).ToListAsync();
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<IEnumerable<User>> GetUsersAssesseeList()
        {
            var userList = await _dbContext.Users.Include(a => a.Role.CompetenciesMap).Include(a => a.Role.RoleType)
                .Include(a => a.Vertical)
                .Include(a => a.Country)
                .Include(a => a.Area)
                .Include(a => a.Region)
                .Where(a => a.Role.RoleType.Roletype == "Assessee").ToListAsync();
            return userList;
        }

        public async Task<long> UpdateUser(User user)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Users.Add(user);
                    _dbContext.Entry(user).State = EntityState.Modified;
                    var cg = await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                    return user.Id;
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

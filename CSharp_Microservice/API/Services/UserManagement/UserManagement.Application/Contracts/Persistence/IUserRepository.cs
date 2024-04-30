using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        //Task<IEnumerable<User>> GetOrdersByUserName(int id);
        Task<long> AddUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(long id);
        Task<User> GetUserOrderById(Expression<Func<User, bool>> predicate = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeString = null, bool disableTracking = true);
        Task<IEnumerable<User>> GetUserByRoleId(long id);

        Task<IEnumerable<User>> GetUsersAssesseeList();
        Task<long> UpdateUser(User user);
    }
}

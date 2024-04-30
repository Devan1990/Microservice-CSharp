using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistence
{
    public interface IverticalRepository : IAsyncRepository<Verticals>
    {
        Task<IEnumerable<Verticals>> GetVerticals();
        Task<Verticals> GetVerticalById(int id);

    }
}

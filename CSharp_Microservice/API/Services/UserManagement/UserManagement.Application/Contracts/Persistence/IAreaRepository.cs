using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistence
{
    public interface IAreaRepository : IAsyncRepository<Area>
    {
        Task<IEnumerable<Area>> GetAreas();
        Task<Area> GetAreaById(int id);

    }
}

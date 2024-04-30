using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistence
{
    public interface IRegionRepository : IAsyncRepository<Region>
    {
        Task<IEnumerable<Region>> GetRegions();
        Task<Region> GetRegionById(int id);
    }
}

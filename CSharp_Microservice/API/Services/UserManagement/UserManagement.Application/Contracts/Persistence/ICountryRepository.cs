using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Contracts.Persistence
{
    public interface ICountryRepository : IAsyncRepository<Country>
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<Country> GetCountryById(int id);

    }
}

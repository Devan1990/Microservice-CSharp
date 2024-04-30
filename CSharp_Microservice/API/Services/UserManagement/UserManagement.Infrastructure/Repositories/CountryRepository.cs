
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
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Country>> GetCountries()
        {
            var countryList = await _dbContext.Countries
                                          .Include(s => s.Areas)
                                          .ThenInclude(s => s.Regions)
                                          .ToListAsync();
            return countryList;
        }

        public async Task<Country> GetCountryById(int id)
        {
            var country = await _dbContext.Countries
                                        .Include(s => s.Areas)
                                        .ThenInclude(s => s.Regions)
                                        .FirstOrDefaultAsync(a => a.Id == id);
            return country;

        }
    }
}

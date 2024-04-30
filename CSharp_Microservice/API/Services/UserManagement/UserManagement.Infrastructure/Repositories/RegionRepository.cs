using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Infrastructure.Repositories
{
    public class RegionRepository : RepositoryBase<Region> , IRegionRepository
    {
        public RegionRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }
            public async Task<IEnumerable<Region>> GetRegions()
            {
                var Regionlist = await _dbContext.Regions
                                              .ToListAsync();
                return Regionlist;
            }

        public async Task<Region> GetRegionById(int id)
        {
            var region = await _dbContext.Regions
                                        .FirstOrDefaultAsync(a => a.Id == id);
            return region;

        }

    }
}

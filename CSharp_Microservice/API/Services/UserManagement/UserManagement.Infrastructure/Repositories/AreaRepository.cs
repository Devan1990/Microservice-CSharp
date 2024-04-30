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
    public class AreaRepository : RepositoryBase<Area> , IAreaRepository
    {
        public AreaRepository(UserManagementContext dbContext) : base(dbContext)
        {
        }
            public async Task<IEnumerable<Area>> GetAreas()
            {
                var AreaList = await _dbContext.Areas
                                              .Include(s => s.Regions)
                                              
                                              .ToListAsync();
                return AreaList;
            }

        public async Task<Area> GetAreaById(int id)
        {
            var Area = await _dbContext.Areas
                                        .Include(s => s.Regions)
                                        //.ThenInclude(s => s.Regions)
                                        .FirstOrDefaultAsync(a => a.Id == id);
            return Area;

        }

    }
}

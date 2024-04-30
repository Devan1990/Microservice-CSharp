using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Domain.Entities;
using CompetencyFramework.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencyFramework.Infrastructure.Repositories
{
    public class CompetencyLevelRepository : RepositoryBase<CompetencyLevel>, ICompetencyLevelRepository
    {
        public CompetencyLevelRepository(CompetencyFrameworkContext dbContext) : base(dbContext)
        {
        }
        public Task<long> AddCompetencyLevel(CompetencyLevel competencyLevel)
        {
            throw new NotImplementedException();
        }

        public async Task<CompetencyLevel> GetCompetencyLevelById(long id)
        {
            var competencyLevel = await _dbContext.CompetencyLevel
                                 .FirstOrDefaultAsync(a => a.Id == id);

            return competencyLevel;

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompetencyLevel>> GetCompetencyLevels()
        {
            var competencyLevels = await _dbContext.CompetencyLevel
                                             .ToListAsync();
            return competencyLevels;


            //throw new NotImplementedException();
        }
    }
}

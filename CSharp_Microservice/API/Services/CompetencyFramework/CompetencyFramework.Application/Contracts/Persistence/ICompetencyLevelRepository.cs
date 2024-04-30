using CompetencyFramework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetencyFramework.Application.Contracts.Persistence
{
    public interface ICompetencyLevelRepository:IAsyncRepository<CompetencyLevel>
    {
        Task<long> AddCompetencyLevel(CompetencyLevel competencyLevel);
        Task<CompetencyLevel> GetCompetencyLevelById(long id);
        Task<IEnumerable<CompetencyLevel>> GetCompetencyLevels();
        
    }
}

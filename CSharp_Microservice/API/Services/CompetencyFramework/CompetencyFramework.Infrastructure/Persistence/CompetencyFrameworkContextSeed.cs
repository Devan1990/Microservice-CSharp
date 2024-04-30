using CompetencyFramework.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencyFramework.Infrastructure.Persistence
{
    public class CompetencyFrameworkContextSeed
    {
        public static async Task SeedAsync(CompetencyFrameworkContext competencyFrameworkContext, ILogger<CompetencyFrameworkContextSeed> logger)
        {
            if (!competencyFrameworkContext.CompetencyLevel.Any())
            {
                competencyFrameworkContext.CompetencyLevel.AddRange(GetDefaultCompetencyLevel());
                await competencyFrameworkContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(CompetencyFrameworkContext).Name);
            }
            await Task.CompletedTask;
        }

        private static IEnumerable<CompetencyLevel> GetDefaultCompetencyLevel()
        {
            return new List<CompetencyLevel>
            {
                new CompetencyLevel() {Name = "Foundational", Weightage = 1},
                 new CompetencyLevel() {Name = "Proficient", Weightage = 2},
                  new CompetencyLevel() {Name = "Advanced", Weightage = 3}
            };
        }
    }
}

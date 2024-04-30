using CompetencyFramework.Domain.Common;
using CompetencyFramework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CompetencyFramework.Infrastructure.Persistence
{
    public class CompetencyFrameworkContext : DbContext
    {
        public CompetencyFrameworkContext(DbContextOptions<CompetencyFrameworkContext> options) : base(options)
        {
        }

        public DbSet<CompetencyGroup> CompetencyGroups { get; set; }
        public DbSet<Competency> Competencies { get; set; }
        public DbSet<Domain.Entities.Attribute> Attributes { get; set; }
        public DbSet<CompetencyLevel> CompetencyLevel { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
                {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "swn";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "swn";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

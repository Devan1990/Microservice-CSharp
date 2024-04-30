using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Persistence
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Verticals> Verticals { get; set; }
        public DbSet<CompetenciesMap> CompetenciesMap { get; set; }

        public DbSet<RoleType> RoleType { get; set; }
        public DbSet<ExpectedLevel> ExpectedLevels { get; set; }

        public DbSet<RoleMapping> RoleMappings { get; set; }

        public DbSet<AssessorRole> AssessorRoles { get; set; }
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

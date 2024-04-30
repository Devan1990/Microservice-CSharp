using Microsoft.EntityFrameworkCore;
using UserManagement.Grpc.Entities;

namespace UserManagement.Grpc.Persistence
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options)
        {
        }

       
        public DbSet<Role> Roles { get; set; }
       

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<EntityBase>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entry.Entity.CreatedDate = DateTime.Now;
        //                entry.Entity.CreatedBy = "swn";
        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedDate = DateTime.Now;
        //                entry.Entity.LastModifiedBy = "swn";
        //                break;
        //        }
        //    }
        //    return base.SaveChangesAsync(cancellationToken);
        //}
    }
}

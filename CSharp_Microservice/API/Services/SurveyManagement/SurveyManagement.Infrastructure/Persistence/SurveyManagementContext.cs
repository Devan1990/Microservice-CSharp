using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using SurveyManagement.Domain.Common;
using SurveyManagement.Domain.Entities;

namespace SurveyManagement.Infrastructure.Persistence
{
    public class SurveyManagementContext : DbContext
    {
        public SurveyManagementContext(DbContextOptions<SurveyManagementContext> options) : base(options)
        {
        }

       
        public DbSet<Survey> Surveys { get; set; }
      //  public DbSet<SurveyRoleMapping> SurveyRoleMappings { get; set; }

        public DbSet<AssessmentSurvey> AssessmentSurveys { get; set; }
        public DbSet<UserSurvey> UserSurveys { get; set; }
        public DbSet<UserSurveyAssessment> UserSurveyAssessments { get; set; }

        public DbSet<AssessmentType> AssessmentTypes { get; set; }
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

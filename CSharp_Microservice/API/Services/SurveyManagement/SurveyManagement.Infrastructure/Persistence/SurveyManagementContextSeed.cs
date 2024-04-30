using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyManagement.Domain.Entities;


namespace SurveyManagement.Infrastructure.Persistence
{
    public class SurveyManagementContextSeed
    {
        public static async Task SeedAsync(SurveyManagementContext surveymanagementContext, ILogger<SurveyManagementContextSeed> logger)
        {
            if (!surveymanagementContext.AssessmentTypes.Any())
            {
                surveymanagementContext.AssessmentTypes.AddRange(GetPreconfiguredAssessmentTypes());
                await surveymanagementContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(SurveyManagementContext).Name);
            }
            await Task.CompletedTask;
        }

        private static IEnumerable<Survey> GetPreconfiguredUsers()
        {
            return new List<Survey>
            {
                //new User() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }
          private static IEnumerable<Survey> GetPreconfiguredRoles()
        {
            return new List<Survey>
            {
                //new User() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }

        private static IEnumerable<AssessmentType> GetPreconfiguredAssessmentTypes()
        {
            return new List<AssessmentType>
            {
                new AssessmentType()
                {
                    Assessmenttype = "Verification Pending",

                },
                 new AssessmentType()
                {
                    Assessmenttype = "Verified",

                },
                 new AssessmentType()
                {
                    Assessmenttype = "Assessment Initiated",

                },
                 new AssessmentType()
                {
                    Assessmenttype = "Work in Progress",

                },
                 new AssessmentType()
                {
                    Assessmenttype = "Completed",

                }
        };
        }
    }
}

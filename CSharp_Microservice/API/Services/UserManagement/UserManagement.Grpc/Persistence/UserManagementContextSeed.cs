using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Grpc.Entities;

namespace UserManagement.Grpc.Persistence
{
    public class UserManagementContextSeed
    {
        public static async Task SeedAsync(UserManagementContext orderContext, ILogger<UserManagementContextSeed> logger)
        {
            //if (!orderContext.Users.Any())
            //{
            //    orderContext.Users.AddRange(GetPreconfiguredOrders());
            //    await orderContext.SaveChangesAsync();
            //    logger.LogInformation("Seed database associated with context {DbContextName}", typeof(UserManagementContext).Name);
            //}
            await Task.CompletedTask;
        }

        private static IEnumerable<Role> GetPreconfiguredUsers()
        {
            return new List<Role>
            {
                //new User() {UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }
        
    }
}

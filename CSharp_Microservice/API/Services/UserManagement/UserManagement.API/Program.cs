using Common.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using UserManagement.API.Extensions;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<UserManagementContext>((context, services) =>
                    {
                        var logger = services.GetService<ILogger<UserManagementContextSeed>>();
                        UserManagementContextSeed
                            .SeedAsync(context, logger)
                            .Wait();
                    })
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog(SeriLogger.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

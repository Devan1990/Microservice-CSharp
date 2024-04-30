using SurveyManagement.API.Extensions;
using Common.Logging;
using Serilog;
using SurveyManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SurveyManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<SurveyManagementContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<SurveyManagementContextSeed>>();
                    SurveyManagementContextSeed
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

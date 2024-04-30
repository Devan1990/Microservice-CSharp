using Microsoft.Extensions.Logging.Configuration;

namespace OcelotApiGateway
{

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                //.MigrateDatabase<SurveyManagementContext>((context, services) =>
                //{
                //    var logger = services.GetService<ILogger<SurveyManagementContextSeed>>
                //    SurveyManagementContextSeed
                //        .SeedAsync(context, logger)
                //        .Wait();
                //})
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //  .UseSerilog(SeriLogger.Configure)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
                })
                 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
              .ConfigureLogging((hostingContext, loggingbuilder) =>
              {
                  loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("logging"));
                  loggingbuilder.AddConsole();
                  loggingbuilder.AddDebug();

              });
    }
}
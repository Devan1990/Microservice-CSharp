using Common.Logging;
using Serilog;
using UserManagement.Grpc.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UserManagement.Grpc.Persistence;

namespace UserManagement.Grpc
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
        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .UseSerilog(SeriLogger.Configure)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }



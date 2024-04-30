
using Microsoft.EntityFrameworkCore;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Infrastructure.Persistence;
using SurveyManagement.Infrastructure.Repositories;
using SurveyManagement.WorkerService;
using SurveyManagement.WorkerService.Grpc;
using SurveyManagement.WorkerService.GrpcServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var config = context.Configuration;
        services.AddDbContext<SurveyManagementContext>(options => options.UseSqlServer(config.GetConnectionString("SurveyConnectionString")), ServiceLifetime.Singleton);
        services.AddSingleton<ISurveyRepository, SurveyRepository>();
        services.AddSingleton<IUserSurveyRepository, UserSurveyRepository>();
        services.AddSingleton<IUserSurveyAssessmentRepository, UserSurveyAssessmentRepository>();
        string url = config["GrpcSettings:UserManagementUrl"].ToString();
        services.AddGrpcClient<RoleProtoService.RoleProtoServiceClient>
                      (o => o.Address = new Uri(url));
        services.AddSingleton<UserSurveyGrpcService>();
      
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

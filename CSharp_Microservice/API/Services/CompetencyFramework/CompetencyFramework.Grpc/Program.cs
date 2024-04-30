using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup;
using CompetencyFramework.Grpc.Services;
using CompetencyFramework.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddAutoMapper(typeof(CompetencyFrameworkService));
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CompetencyFrameworkService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

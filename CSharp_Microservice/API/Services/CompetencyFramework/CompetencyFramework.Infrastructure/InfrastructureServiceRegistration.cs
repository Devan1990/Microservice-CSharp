using CompetencyFramework.Application.Contracts.Infrastructure;
using CompetencyFramework.Application.Contracts.Persistence;
using CompetencyFramework.Application.Features.CompetencyGroup.Queries.GetCompetencyGroup;
using CompetencyFramework.Application.Models;
using CompetencyFramework.Infrastructure.Mail;
using CompetencyFramework.Infrastructure.Persistence;
using CompetencyFramework.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CompetencyFramework.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<CompetencyFrameworkContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CompetencyFrameworkConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));                        
            services.AddScoped<ICompetencyGroupRepository, CompetencyGroupRepository>();
            services.AddScoped<ICompetencyRepository, CompetencyRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<ICompetencyLevelRepository, CompetencyLevelRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SurveyManagement.Application.Contracts.Infrastructure;
using SurveyManagement.Application.Contracts.Persistence;
using SurveyManagement.Application.Models;
using SurveyManagement.Infrastructure.Mail;
using SurveyManagement.Infrastructure.Persistence;
using SurveyManagement.Infrastructure.Repositories;

namespace SurveyManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SurveyManagementContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SurveyConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<ISurveyRepository, SurveyRepository>();
          //  services.AddScoped<ISurveyRoleMappingRepository, SurveyRoleMappingRepository>();
            services.AddScoped<IUserSurveyRepository, UserSurveyRepository>();
            services.AddScoped<IUserSurveyAssessmentRepository, UserSurveyAssessmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAssessmentSurveyRepository, AssessmentSurveyRepository>();
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}

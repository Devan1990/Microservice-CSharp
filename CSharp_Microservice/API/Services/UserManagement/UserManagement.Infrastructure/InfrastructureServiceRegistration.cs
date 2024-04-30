using CompetencyFramework.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Application.Contracts.Infrastructure;
using UserManagement.Application.Contracts.Persistence;
using UserManagement.Application.Models;
using UserManagement.Infrastructure.Mail;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<UserManagementContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UsersConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));                        
            services.AddScoped<IUserRepository, UserManagementRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IverticalRepository,VerticalRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRoleMappingRepository, RoleMappingRepository>();
            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}

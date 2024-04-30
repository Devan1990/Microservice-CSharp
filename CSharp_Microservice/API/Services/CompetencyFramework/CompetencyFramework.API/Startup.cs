using CompetencyFramework.API.EventBusConsumer;
using CompetencyFramework.Application;
using CompetencyFramework.Infrastructure;
using CompetencyFramework.Infrastructure.Persistence;
using EventBus.Messages.Common;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
//using NSwag;
//using NSwag.AspNetCore;
//using NSwag.Generation.Processors.Security;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
namespace CompetencyFramework.API
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IConfiguration configuration)
        {
            this.HostingEnvironment = env;
            Configuration = configuration;
        }

        public Microsoft.AspNetCore.Hosting.IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

           // var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddApplicationServices();
            
            services.AddInfrastructureServices(Configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // General Configuration
            services.AddScoped<BasketCheckoutConsumer>();
            services.AddAutoMapper(typeof(Startup));
       
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CompetencyFramework.API", Version = "v1" });
            });

            services.AddHealthChecks()
                    .AddDbContextCheck<CompetencyFrameworkContext>();

            //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            //    .AddAzureADBearer(options => this.Configuration.Bind("AzureAd", options));

            //if (this.HostingEnvironment.IsDevelopment())
            //{
            //    services.AddTransient<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, DisableAuthorizationHandler<Microsoft.AspNetCore.Authorization.IAuthorizationRequirement>>();
            //}
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                Configuration.Bind("AzureAd", options);
                // Authority will be Your AzureAd Instance and Tenant Id
                options.Authority = $"https://sts.windows.net/" + $"{Configuration["AzureAd:TenantId"]}/";

                // The valid audiences are both the Client ID(options.Audience) and api://{ClientID}
                options.TokenValidationParameters.ValidAudiences = new string[] { Configuration["AzureAd:ClientId"], $"api://{Configuration["AzureAd:ClientId"]}" };
            });
            AddSwagger(services);

            services.AddCors(o => o.AddPolicy("ComentencyManagementCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }
        private void AddSwagger(IServiceCollection services)
        {
            services.AddOpenApiDocument(document =>
            {
                document.AddSecurity("bearer", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "Azure AAD Authentication",
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Flows = new NSwag.OpenApiOAuthFlows()
                    {
                        Implicit = new NSwag.OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                {
                    { $"api://{Configuration["AzureAd:ClientId"]}/ReadAccess", "Access Application" },
                },
                            AuthorizationUrl = $"{Configuration["AzureAd:Instance"]}{Configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize",
                            TokenUrl = $"{Configuration["AzureAd:Instance"]}{Configuration["AzureAd:TenantId"]}/oauth2/v2.0/token",
                        },
                    },
                });

                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CompetencyFramework.API v1"));
                
            }
            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    // Use the same client id as your application.
                    // Alternatively you can register another application in the portal and use that as client id
                    // Doing that you will have to create a client secret to access that application and get into space of secret management
                    // This makes it easier to access the application and grab a token on behalf of user
                    ClientId = Configuration["AzureAd:ClientId"],
                    AppName = "competencyassessmenttool/API",
                };
            });
            app.UseHttpsRedirection();
            //app.UseCors("corspolicy");
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseRouting();
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
             
            });
        }
    }
}

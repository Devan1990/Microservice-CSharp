using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using SurveyManagement.API.EventBusConsumer;
using SurveyManagement.Application;
using Microsoft.Extensions.DependencyInjection;
using SurveyManagement.Infrastructure;
using SurveyManagement.Infrastructure.Persistence;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SurveyManagement.API.GrpcServices;
using UserManagement.Grpc.Protos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NSwag;
using NSwag.Generation.Processors.Security;
using NSwag.AspNetCore;

namespace SurveyManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
            services.AddControllers().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            // MassTransit-RabbitMQ Configuration
            //services.AddMassTransit(config => {

            //    config.AddConsumer<BasketCheckoutConsumer>();

            //    config.UsingRabbitMq((ctx, cfg) => {
            //        cfg.Host(Configuration["EventBusSettings:HostAddress"]);
            //        cfg.UseHealthCheck(ctx);

            //        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
            //            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
            //        });
            //    });
            //});
            //services.AddMassTransitHostedService();

            // General Configuration
            // services.AddScoped<BasketCheckoutConsumer>();
            services.AddAutoMapper(typeof(Startup));

            // Grpc Configuration
            services.AddGrpcClient<RoleProtoService.RoleProtoServiceClient>
                        (o => o.Address = new Uri(Configuration["GrpcSettings:RoleUrl"]));

            services.AddScoped<RoleGrpcService>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "SurveyManagement.API", Version = "v1" });
            });

            services.AddHealthChecks()
                    .AddDbContextCheck<SurveyManagementContext>();
            //enable cors
            //services.AddCors(p => p.AddPolicy("corspolicy", build =>
            //{
            //    build.WithOrigins("http://127.0.0.1:5173/").AllowAnyMethod().AllowAnyHeader();
            //}));
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

            services.AddCors(o => o.AddPolicy("SurveyManagementCorsPolicy", builder =>
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SurveyManagement.API v1"));
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
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseCors("corspolicy");
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

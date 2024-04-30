using CAT.BFF.Services;
using Common.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using Polly;
using Polly.Extensions.Http;
using Serilog;

namespace CAT.BFF
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<LoggingDelegatingHandler>();

            services.AddHttpClient<ICompetencyFrameworkService, CompetencyFrameworkService>(c =>
                c.BaseAddress = new Uri(configRoot["ApiSettings:CompetencyFrameworkUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddHttpClient<IUserManagementService, UserManagementService>(c =>
                c.BaseAddress = new Uri(configRoot["ApiSettings:UserManagementUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

          

            services.AddHttpClient<ISurveyManagementService, SurveyManagementService>(c =>
                c.BaseAddress = new Uri(configRoot["ApiSettings:SurveyManagementUrl"]))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddCors();
            services.AddControllers();
          
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CAT.BFF", Version = "v1" });
            });
           
          
            services.AddHealthChecks()
                .AddUrlGroup(new Uri($"{configRoot["ApiSettings:CompetencyFrameworkUrl"]}/swagger/index.html"), "CompetencyFramework.API", HealthStatus.Degraded)
                .AddUrlGroup(new Uri($"{configRoot["ApiSettings:UserManagementUrl"]}/swagger/index.html"), "UserManagement.API", HealthStatus.Degraded)
                .AddUrlGroup(new Uri($"{configRoot["ApiSettings:SurveyManagementUrl"]}/swagger/index.html"), "SurveyManagement.API", HealthStatus.Degraded);
            //var identityUrl = configRoot.GetValue<string>("IdentityUrl");
            //var callBackUrl = configRoot.GetValue<string>("CallBackUrl");
            //var sessionCookieLifetime = configRoot.GetValue("SessionCookieLifetimeMinutes", 60);
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                configRoot.Bind("AzureAd", options);
                // Authority will be Your AzureAd Instance and Tenant Id
                options.Authority = $"https://sts.windows.net/" + $"{configRoot["AzureAd:TenantId"]}/";

                // The valid audiences are both the Client ID(options.Audience) and api://{ClientID}
                options.TokenValidationParameters.ValidAudiences = new string[] { configRoot["AzureAd:ClientId"], $"api://{configRoot["AzureAd:ClientId"]}" };
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
                    { $"api://{configRoot["AzureAd:ClientId"]}/ReadAccess", "Access Application" },
                },
                            AuthorizationUrl = $"{configRoot["AzureAd:Instance"]}{configRoot["AzureAd:TenantId"]}/oauth2/v2.0/authorize",
                            TokenUrl = $"{configRoot["AzureAd:Instance"]}{configRoot["AzureAd:TenantId"]}/oauth2/v2.0/token",
                        },
                    },
                });

                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CAT.BFF v1"));
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
                    ClientId = configRoot["AzureAd:ClientId"],
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

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            // In this case will wait for
            //  2 ^ 1 = 2 seconds then
            //  2 ^ 2 = 4 seconds then
            //  2 ^ 3 = 8 seconds then
            //  2 ^ 4 = 16 seconds then
            //  2 ^ 5 = 32 seconds

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        Log.Error($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}.");
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30)
                );
        }
    }
}

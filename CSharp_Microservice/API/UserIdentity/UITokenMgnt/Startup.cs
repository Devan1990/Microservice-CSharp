using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using UITokenMgnt.Services;

namespace UITokenMgnt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "UITokenMgnt.API", Version = "v1" });
            });
            //services.AddScoped<ApiService>();
            //services.AddTransient<ApiService>();
            //services.AddHttpClient();

            //services.AddOptions();

            string[] initialScopes = Configuration.GetValue<string>("CallApi:ScopeForAccessToken")?.Split(' ');

            services.AddMicrosoftIdentityWebAppAuthentication(Configuration)
                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
                .AddInMemoryTokenCaches();

            ////services.AddRazorPages().AddMvcOptions(options =>
            ////{
            ////    var policy = new AuthorizationPolicyBuilder()
            ////        .RequireAuthenticatedUser()
            ////        .Build();
            ////    options.Filters.Add(new AuthorizeFilter(policy));
            ////}).AddMicrosoftIdentityUI();

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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UITokenMgnt.API v1"));
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
                    AppName = "CompentencyAssessmentTool-UIEnvironment",
                };
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();


            });
        }
    }
}


using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using UserIdentity.Data;

namespace UserIdentity
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

            // var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = Configuration["AzureAd:ClientId"];
                    microsoftOptions.ClientSecret = Configuration["AzureAd:ClientSecret"];
                });


            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var identityUrl = Configuration.GetValue<string>("AzureAd:Instance")+"/"+ Configuration.GetValue<string>("AzureAd:TenantId");
            var callBackUrl = Configuration.GetValue<string>("AzureAd:callBackUrl");
            var sessionCookieLifetime = Configuration.GetValue("SessionCookieLifetimeMinutes", 60);
            var ClientId = Configuration.GetValue<string>("AzureAd:ClientId");
            // Add Authentication services

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityUrl.ToString();
                options.SignedOutRedirectUri = callBackUrl.ToString();
                options.ClientId = ClientId;
                options.ClientSecret = "secret";
                options.ResponseType =  "code id_token";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = false;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("orders");
                options.Scope.Add("basket");
                options.Scope.Add("marketing");
                options.Scope.Add("locations");
                options.Scope.Add("webshoppingagg");
                options.Scope.Add("orders.signalrhub");
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserIdentity.API", Version = "v1" });
            });

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();
            

        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserIdentity.API v1"));
            }
            
            app.UseHttpsRedirection();
            //app.UseCors("corspolicy");
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
    }
}

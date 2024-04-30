
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace UserIdentification
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void Configure(IApplicationBuilder app)
        {

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var identityUrl = Configuration.GetValue<string>("AzureAd:Instance") + "/" + Configuration.GetValue<string>("AzureAd:TenantId");
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
                options.ResponseType = "code id_token";
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
        }
    }
}

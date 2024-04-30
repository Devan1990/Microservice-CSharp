using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot();
        }
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                await app.UseOcelot();
                // app.UseSwagger();

                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CompetencyFramework.API v1"));
            }
            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    //endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
            //    //{
            //    //    Predicate = _ => true,
            //    //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //    //});
            //});
            //add middleware pipeline
           
        }
    }
}
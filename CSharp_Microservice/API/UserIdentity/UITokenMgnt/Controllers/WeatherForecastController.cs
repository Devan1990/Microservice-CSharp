using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UITokenMgnt.Services;

namespace UITokenMgnt.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
          
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            
            

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("GetTokenData")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetToken()
        {

             string clientid = "2c9bfbcc-7538-41b0-8cdf-6fde140c41be";
            string TenantId = "05d75c05-fa1a-42e7-9cf1-eb416c396f2d";
             string authority = "https://login.microsoftonline.com/"+ TenantId;
             string clientsecret = "K1t8Q~3LCW2XVqKonAWjzPgx.VDwY2Ui378-lbGJ";
             string resource = "api://7811113a-d2b2-4989-9ac3-ec6e9d81528f";
            AuthenticationContext authenticationContext = new AuthenticationContext(authority);
            ClientCredential clientcredential = new ClientCredential(clientid, clientsecret);
            string token = authenticationContext.AcquireTokenAsync(resource, clientcredential).Result.AccessToken;

            //var groups = await _apiService.GetApiDataAsync();
            //if (groups == null)
            //{
            //    return NotFound();
            //}
            return Ok(token);
        }
    }
}
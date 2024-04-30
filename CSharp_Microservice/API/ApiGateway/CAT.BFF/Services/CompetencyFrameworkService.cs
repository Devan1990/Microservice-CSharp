using CATBFF.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace CAT.BFF.Services
{
    public class CompetencyFrameworkService : ICompetencyFrameworkService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        //static string clientid = "7811113a-d2b2-4989-9ac3-ec6e9d81528f";
        //static string authority = "https://login.microsoftonline.com/05d75c05-fa1a-42e7-9cf1-eb416c396f2d";
        //static string clientsecret = "DYs8Q~yyHOhneELvWw4Ara-L8XVVzJphVRWkIaYB";
        //static string resource = "api://7811113a-d2b2-4989-9ac3-ec6e9d81528f";
        public CompetencyFrameworkService(HttpClient client, IConfiguration configuration)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _configuration = configuration;
        }

        public async Task<IEnumerable<CompetencyGroupsVm>> GetCompetencyGroup(string tkn)
        {
            string CompetencyFrameworkUrl = _configuration["ApiSettings:CompetencyFrameworkUrl"].ToString();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tkn);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, CompetencyFrameworkUrl + "/api/v1/CompetencyGroup/GetCompetencyGroups");
            requestMessage.Content = new StringContent(String.Empty, System.Text.Encoding.UTF8, "application/json");
            var res = await _client.SendAsync(requestMessage);
            var result = await res.ReadContentAs<List<CompetencyGroupsVm>>();
            return result;


            ////AuthenticationContext authenticationContext = new AuthenticationContext(authority);
            ////ClientCredential clientcredential = new ClientCredential(clientid, clientsecret);
            ////string token = authenticationContext.AcquireTokenAsync(resource, clientcredential).Result.AccessToken;
            //_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tkn);
            ////var response = await _client.GetAsync($"/api/v1/CompetencyGroup/GetCompetencyGroups"););
            //HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:5001/api/v1/CompetencyGroup/GetCompetencyGroups");
            //requestMessage.Content = new StringContent(String.Empty, System.Text.Encoding.UTF8, "application/json");
            //var res = await _client.SendAsync(requestMessage);
            ////_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tkn);
            ////var response = await _client.GetAsync($"/api/v1/CompetencyGroup/GetCompetencyGroups");
            //var result = await res.ReadContentAs<List<CompetencyGroupsVm>>();
            //return result;
        }
    }
}

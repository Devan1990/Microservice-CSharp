using CAT.BFF.Models;
using CATBFF.Models;

namespace CAT.BFF.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public UserManagementService(HttpClient client, IConfiguration configuration)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _configuration = configuration;
        }

        public async Task<IEnumerable<RoleVm2>> GetRoles(string token)
        {
            string UserManagementUrl = _configuration["ApiSettings:UserManagementUrl"].ToString();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, UserManagementUrl + "/api/v1/RoleManagement/GetRoles");
            requestMessage.Content = new StringContent(String.Empty, System.Text.Encoding.UTF8, "application/json");
            var res = await _client.SendAsync(requestMessage);
            var result = await res.ReadContentAs<List<RoleVm2>>();
            return result;
            //var response = await _client.GetAsync($"/api/v1/RoleManagement/GetRoles");
            //var result = await response.ReadContentAs<List<RoleVm2>>();
            //return result;
        }

        public async Task<IEnumerable<ExpectedLevelVm>> GetExpectedLevels()
        {
            var response = await _client.GetAsync($"/api/v1/RoleManagement/GetExpectedLevels");
            var result = await response.ReadContentAs<List<ExpectedLevelVm>>();
            return result;
        }

        public async Task<IEnumerable<UsersVm>> GetUsers()
        {
            var response = await _client.GetAsync($"/api/v1/UserManagement/GetUsers");
            var result = await response.ReadContentAs<List<UsersVm>>();
            return result;
        }

        public async Task<RoleVm2> GetRole(long id)
        {
            var response = await _client.GetAsync($"/api/v1/RoleManagement/GetRole/" + id);
            var result = await response.ReadContentAs<RoleVm2>();
            return result;
        }
    }
}

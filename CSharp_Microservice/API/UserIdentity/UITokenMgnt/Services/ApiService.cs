using Microsoft.Identity.Web;
//using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace UITokenMgnt.Services
{
    public class ApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _configuration;
        static string clientid = "2c9bfbcc-7538-41b0-8cdf-6fde140c41be";
        static string authority = "https://login.microsoftonline.com/05d75c05-fa1a-42e7-9cf1-eb416c396f2d";
        static string clientsecret = "K1t8Q~3LCW2XVqKonAWjzPgx.VDwY2Ui378-lbGJ";
        static string resource = "api://2c9bfbcc-7538-41b0-8cdf-6fde140c41be";
        public ApiService(IHttpClientFactory clientFactory,
            ITokenAcquisition tokenAcquisition,
            IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _tokenAcquisition = tokenAcquisition;
            _configuration = configuration;
        }

        public async Task<string> GetApiDataAsync()
        {
            var success = false;
            string tkn = null;
            
            try
            {
                var client = _clientFactory.CreateClient("HttpClientWithSSLUntrusted");

                var scope = _configuration["CallApi:ScopeForAccessToken"];
               
                var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { scope });
                //AuthenticationContext authenticationContext = new AuthenticationContext(authority);
                //ClientCredential clientcredential = new ClientCredential(clientid, clientsecret);
                //string token = authenticationContext.AcquireTokenAsync(resource, clientcredential).Result.AccessToken;
                tkn = accessToken.ToString();
                return tkn;
            }
            catch (MicrosoftIdentityWebChallengeUserException e)
            {
                tkn = $"{e.GetType()}: {e.Message}{Environment.NewLine}Stacktrace:{Environment.NewLine}{e.StackTrace}{Environment.NewLine}{Environment.NewLine}Logout then login again to fix the issue.";
            }
            catch (Exception e)
            {
                tkn = $"{e.GetType()}: {e.Message}{Environment.NewLine}Stacktrace:{e.StackTrace}";
            }

            return tkn;
        }
    }
}

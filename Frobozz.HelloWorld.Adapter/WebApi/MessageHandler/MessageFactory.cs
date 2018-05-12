using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xlent.Lever.Authentication.Sdk;
using Xlent.Lever.Libraries2.Core.MultiTenant.Model;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;

namespace Frobozz.HelloWorld.Adapter.MessageHandler
{
    public class MessageFactory
    {
        public static async Task<HttpRequestMessage> GeneratePostRequestMessage(string relativeUrl, object content)
        {

            var url = $"{ConfigurationManager.AppSettings["BtApiBaseUrl"]}/{relativeUrl}";
            var token = await GenerateAccessToken();

            var serializedContent = JsonConvert.SerializeObject(content);
            var message = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(serializedContent, Encoding.UTF8, "application/json"),

                Headers =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken),
                }
            };

            return message;
        }

        private static async Task<AuthenticationToken> GenerateAccessToken()
        {
            string organization = ConfigurationManager.AppSettings["Organization"];
            string environment = ConfigurationManager.AppSettings["Environment"];
            var tenant = new Tenant(organization, environment);

            string authenticationUrl = ConfigurationManager.AppSettings["AuthenticationUrl"];
            string authenticationClientId = ConfigurationManager.AppSettings["AuthenticationClientId"];
            string authenticationClientSecret = ConfigurationManager.AppSettings["AuthenticationClientSecret"];
            AuthenticationCredentials authServiceCredentials = new AuthenticationCredentials { ClientId = "user", ClientSecret = "pwd" };
            AuthenticationCredentials authTokenCredentials = new AuthenticationCredentials { ClientId = authenticationClientId, ClientSecret = authenticationClientSecret };
            AuthenticationManager.CreateTokenRefresher(tenant, authenticationUrl, authServiceCredentials, authTokenCredentials);

            var tokenRefresher = AuthenticationManager.CreateTokenRefresher(tenant, authenticationUrl, authServiceCredentials, authTokenCredentials);

            return await tokenRefresher.GetJwtTokenAsync();
        }
    }
}
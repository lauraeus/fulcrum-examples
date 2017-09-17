using System.Configuration;
using System.Threading.Tasks;
using Xlent.Lever.Authentication.Sdk;
using Xlent.Lever.Configurations.Sdk;
using Xlent.Lever.Libraries2.Core.MultiTenant.Model;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;

namespace Frobozz.HelloWorld.Adapter
{
    internal class ConfigurationSetup
    {
        /*public static readonly string ServiceName = "HelloWorldExample";

        private static readonly string Organization = ConfigurationManager.AppSettings["Organization"];
        private static readonly string Environment = ConfigurationManager.AppSettings["Environment"];
        private static readonly string AuthenticationUrl = ConfigurationManager.AppSettings["AuthenticationUrl"];
        private static readonly string AuthenticationClientId = ConfigurationManager.AppSettings["AuthenticationClientId"];
        private static readonly string AuthenticationClientSecret = ConfigurationManager.AppSettings["AuthenticationClientSecret"];

        private static readonly AuthenticationCredentials AuthServiceCredentials = new AuthenticationCredentials { ClientId = "user", ClientSecret = "pwd" };
        private static readonly AuthenticationCredentials AuthTokenCredentials = new AuthenticationCredentials { ClientId = AuthenticationClientId, ClientSecret = AuthenticationClientSecret };*/

        /*   public static Task Register()
        {
         var tenant = new Tenant(Organization, Environment);
            var tokenRefresher = AuthenticationManager.CreateTokenRefresher(tenant, AuthenticationUrl, AuthServiceCredentials, AuthTokenCredentials);
    }*/
}
}
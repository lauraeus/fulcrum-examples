using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Xlent.Lever.Authentication.Sdk.Attributes;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;

namespace Frobozz.HelloWorld.Adapter.Controllers
{
    //You may protect your whole controller with specific user roles
    //[FulcrumAuthorize(AuthenticationRoleEnum.InternalSystemUser, AuthenticationRoleEnum.ExternalSystemUser)]
    [RoutePrefix("api/v1/Example")]
    public class ExampleController : ApiController
    {
        private static readonly string AuthenticationClientId = ConfigurationManager.AppSettings["AuthenticationClientId"];
        private static readonly string AuthenticationClientSecret = ConfigurationManager.AppSettings["AuthenticationClientSecret"];

        private static readonly AuthenticationCredentials AuthTokenCredentials = new AuthenticationCredentials { ClientId = AuthenticationClientId, ClientSecret = AuthenticationClientSecret };

        private static readonly HttpClient HttpClient = new HttpClient();


        //Or you may choose to protect individual methods with user roles.
        [FulcrumAuthorize(AuthenticationRoleEnum.InternalSystemUser)]
        [HttpGet]
        [Route("Get/{id}")]
        public string Get(string id)
        {
            return id;
        }

        [HttpPost]
        [Route("Post")]
        public async Task<string> Post()
        {
            //Retrieve a token using credentials
            var result = await HttpClient.PostAsJsonAsync("http://localhost/Bt.Fulcrum.Api.Sl.WebApi/api/v1/Authentication/Tokens",
                AuthTokenCredentials);

            var response = await result.Content.ReadAsStringAsync();
          
            var obj = JObject.Parse(response);
            var token = obj["Token"].ToString();
            return token;
        }





    }
}
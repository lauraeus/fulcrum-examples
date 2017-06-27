using System.Web.Http;
using Acme.FulcrumFacade.Sl.WebApi.App_Start;

namespace Acme.FulcrumFacade.Sl.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SwaggerConfig.Register);
            GlobalConfiguration.Configure(AutofacConfig.Register);
            GlobalConfiguration.Configuration.EnsureInitialized();

            AutomapperConfig.RegisterMappings();
        }
    }
}

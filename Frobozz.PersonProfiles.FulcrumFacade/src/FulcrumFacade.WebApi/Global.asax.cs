using System.Web.Http;
using Frobozz.PersonProfiles.FulcrumFacade.WebApi;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi
{
    /// <summary>
    /// Starting point for the entire application. See https://stackoverflow.com/questions/2340572/what-is-the-purpose-of-global-asax-in-asp-net for more information.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Initialize the application.
        /// </summary>
        /// <remarks>
        /// Fired when the first instance of the HttpApplication class is created. It allows you to create objects that are accessible by all HttpApplication instances.
        /// </remarks>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(SwaggerConfig.Register);
            GlobalConfiguration.Configure(AutofacConfig.Register);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        /// <summary>
        /// The last possibility to take care of unhandled exceptions.
        /// </summary>
        /// <remarks>
        /// Fired when an unhandled exception is encountered within the application
        /// </remarks>
        protected void Application_Error()
        {
            // TODO: Unhandled exceptions will end up here, so this is a good point to log them.
        }
    }
}

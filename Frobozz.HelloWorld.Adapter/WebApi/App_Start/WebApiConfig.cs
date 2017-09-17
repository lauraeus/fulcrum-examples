using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Xlent.Lever.Authentication.Sdk.Handlers;
using Xlent.Lever.Libraries2.Core.Context;
using Xlent.Lever.Libraries2.WebApi.Pipe.Inbound;

namespace Frobozz.HelloWorld.Adapter
{
    /// <summary>
    /// Configuration class responsible for WebApi specific configurations.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers WebApi specific components.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            //Replace exceptionhandler
            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new ConvertExceptionToFulcrumResponse());

            RegisterMessageHandlers(config);

        }
 
        private static void RegisterMessageHandlers(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new SaveCorrelationId());

            //Register tokenvalidationhandler
            config.MessageHandlers.Add(new TokenValidationHandler());
        }
    }
}

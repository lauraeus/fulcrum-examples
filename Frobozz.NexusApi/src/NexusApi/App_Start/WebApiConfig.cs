using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Nexus.Link.Libraries.Core.Context;
using Nexus.Link.Libraries.Web.Pipe.Inbound;

namespace Frobozz.NexusApi
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
            //Routing will be correspond to the specified Route attribute for each method
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            //Declare the project to return JSON instead of XML
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //If a null value exists, ignore value
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            var correlationIdValueProvider = new CorrelationIdValueProvider();

            RegisterFilters(config);
            RegisterServices(config);
            RegisterMessageHandlers(config);
        }

        #region Registration Methods
        private static void RegisterServices(HttpConfiguration config)
        {
            //Register implementation of IExceptionHandler
            config.Services.Replace(typeof(IExceptionHandler), new ConvertExceptionToFulcrumResponse());
        }

        private static void RegisterFilters(HttpConfiguration config)
        {
            //Register Global Filter for ModelValidation
            config.Filters.Add(new ModelValidation());
        }

        private static void RegisterMessageHandlers(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new SaveCorrelationId());
        }
        #endregion
    }

    /// <summary>
    /// Route provider that allows inheritance of routing attributes.
    /// </summary>
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        /// <inheritdoc />
        protected override IReadOnlyList<IDirectRouteFactory>
            GetActionRouteFactories(HttpActionDescriptor actionDescriptor)
        {
            // inherit route attributes decorated on base class controller's actions
            return actionDescriptor.GetCustomAttributes<IDirectRouteFactory>
                (inherit: true);
        }
    }
}

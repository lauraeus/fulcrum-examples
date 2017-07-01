using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SupplierCompany.SystemFacade.Bll;
using SupplierCompany.SystemFacade.Dal.WebApi.GoogleGeocode.Clients;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace SupplierCompany.SystemFacade.Sl.WebApi
{
    /// <summary>
    /// Connect interfaces with implementations.
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Register how interfaces are connected with implementations.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            //Register Bll and Dal dependencies
            builder.RegisterType<GeocodingFunctionality>().As<IGeocodingFunctionality>();
            builder.RegisterType<GeocodeClient>().As<IGeocodeClient>();
            builder.Register(ctx => new GeocodeClient(new RestClient("https://maps.googleapis.com/maps/api/geocode")))
                .As<IGeocodeClient>().SingleInstance();

            // Register your controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
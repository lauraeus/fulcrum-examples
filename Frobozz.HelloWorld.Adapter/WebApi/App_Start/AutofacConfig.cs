using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace Frobozz.HelloWorld.Adapter
{
    /// <summary>
    /// IoC container registrations are done here
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Register all dependencies and controllers
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            //RegisterLogicLayer(builder);
            //RegisterDataAccessLayer(builder);

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }



        /*private static void RegisterDataAccessLayer(ContainerBuilder builder)
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

            builder.RegisterType<ProductEventsDataAccess>().As<IProductEventsDataAccess>().WithParameters(new List<Parameter>
            {
                new NamedParameter("baseUrl", ConfigurationManager.AppSettings["ApiUrl"])
            });

            builder.RegisterType<CustomerOrderEventsDataAccess>().As<ICustomerOrderEventsDataAccess>().WithParameters(new List<Parameter>
            {
                new NamedParameter("baseUrl", ConfigurationManager.AppSettings["ApiUrl"])
            }); 

            builder.RegisterType<SynchronizedEventsDataAccess>().As<ISynchronizedEntityEventsDataAccess>().WithParameters(new List<Parameter>
            {
                new NamedParameter("baseUrl", ConfigurationManager.AppSettings["ApiUrl"])
            });
        }*/
        //#endregion
    }
}
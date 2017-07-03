using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.PersonProfiles.Bll;
using Frobozz.PersonProfiles.Dal.Contracts.PersonProfile;
using Frobozz.PersonProfiles.Dal.MemoryStorage.PersonProfile;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi
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
            builder.RegisterType<PersonProfilesFunctionality>().As<IPersonProfilesFunctionality>();
            builder.RegisterType<PersonProfilePersistance>().As<IPersonProfilePersistance<IStorablePersonProfile>>();

            // Register your controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
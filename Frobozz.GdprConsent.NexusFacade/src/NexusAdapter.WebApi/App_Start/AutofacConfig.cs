using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Mock;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.SqlServer;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi
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

            // Register GDPR capability
            var gdprCapability = CreateGdprCapability(useMock: false);
            builder.Register(ctxt => gdprCapability)
                .As<IGdprCapability>()
                .SingleInstance();

            // Register your controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IGdprCapability CreateGdprCapability(bool useMock = false)
        {
            IStorage storage;
            if (useMock)
            {
                storage = new MemoryStorage();
            }
            else
            {
                storage = new SqlServerStorage("Data Source=localhost;Initial Catalog=LeverExampleGdpr;Persist Security Info=True;User ID=LeverExampleGdprUser;Password=Password123!;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True");
            }

            return new Mapper(storage);
        }
    }
}
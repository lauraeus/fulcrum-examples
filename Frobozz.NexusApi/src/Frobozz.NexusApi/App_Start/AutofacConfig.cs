using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.NexusApi.Capabilities;
using Frobozz.NexusApi.RestServices.GdprCapability;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.NexusApi
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
#if true
            var personStorage = new MemoryPersistance<Person, string>();
            var consentStorage = new MemoryManyToOnePersistance<Consent, string>(consent => consent.PersonId);
            var personConsentStorage = consentStorage;
#else
            var personStorage = new PersonService("http://localhost/GdprConsent.NexusFacade.WebApi/api/Persons");
            var consentStorage = new ConsentService("http://localhost/GdprConsent.NexusFacade.WebApi/api/Consents");
            var personConsentStorage = new PersonConsentService("http://localhost/GdprConsent.NexusFacade.WebApi/api/Persons");
#endif
            var gdprCapability = new GdprCapability(personStorage, consentStorage, personConsentStorage);
            //Register Bll and Dal dependencies
            builder.Register(ctxt => gdprCapability)
                .As<IGdprCapability>()
                .SingleInstance();

            // Register your controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
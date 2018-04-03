using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.NexusApi.Capabilities;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;

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

            var personStorage = new MemoryPersistance<Person, string>();
            var consentStorage = new MemoryManyToOnePersistance<Consent, string>(consent => consent.PersonId);
            var gdprCapability = new GdprCapability(personStorage, consentStorage, consentStorage);
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
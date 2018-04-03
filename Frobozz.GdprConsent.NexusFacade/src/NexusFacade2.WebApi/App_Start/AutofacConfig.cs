using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi
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

            var personStorage = new MemoryPersistance<PersonTable, Guid>();
            var consentStorage = new MemoryPersistance<ConsentTable, Guid>();
            var addressStorage = new MemoryManyToOnePersistance<AddressTable, Guid>(item => item.PersonId);
            var personConsentStorage =
                new MemoryManyToManyPersistance<PersonConsentTable, PersonTable, ConsentTable, Guid>(
                    pc => pc.PersonId,
                    pc => pc.ConsentId, 
                    personStorage,
                    consentStorage);
            var storage = new Storage(personStorage, addressStorage, consentStorage, personConsentStorage);
            //Register Bll and Dal dependencies
            builder.Register(ctx => storage)
                .As<IStorage>()
                .SingleInstance();

            // Register your controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

           //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
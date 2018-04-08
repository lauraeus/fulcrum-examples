using System;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Frobozz.GdprConsent.NexusFacade.WebApi.Logic;
using Frobozz.GdprConsent.NexusFacade.WebApi.Mappers;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

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

            var personStorage = new CrudMemory<PersonTable, Guid>();
            var consentStorage = new CrudMemory<ConsentTable, Guid>();
            var addressStorage = new ManyToOneMemory<AddressTable, Guid>(item => item.PersonId);
            var personConsentStorage =
                new ManyToOneMemory<PersonConsentTable, Guid>(item => item.PersonId);
            var storage = new Storage(personStorage, addressStorage, consentStorage, personConsentStorage);
            //Register Bll and Dal dependencies
            builder.Register(ctx => storage)
                .As<IStorage>()
                .SingleInstance();

            var personLogic = new PersonLogic(storage);
            var consentLogic = new CrudMapper<Consent,string,IStorage,ConsentTable,Guid>(storage, storage.Consent, new ConsentMapper());
            var personConsentLogic =
                new ManyToOneMapper<PersonConsent, string, IStorage, PersonConsentTable, Guid>(storage,
                    storage.PersonConsent, new PersonConsentMapper());
            var gdprCapability = new GdprCapability(personLogic, consentLogic, personConsentLogic);
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
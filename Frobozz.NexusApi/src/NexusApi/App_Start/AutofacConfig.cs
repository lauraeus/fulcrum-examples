using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.NexusApi.Bll.Gdpr.Caches;
using Frobozz.NexusApi.Bll.Gdpr.ClientTranslators;
using Frobozz.NexusApi.Bll.Gdpr.ServerTranslators;
using Frobozz.NexusApi.Dal.Mock.Translator;

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

            // Register GDPR capability
            var gdprCapability = CreateGdprCapability(useMock: true);
            builder.Register(ctxt => gdprCapability)
                .As<IGdprCapability>()
                .SingleInstance();

            // Register controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IGdprCapability CreateGdprCapability(bool useMock = false)
        {
            var translationService = new TranslatorServiceMock();
            IGdprCapability dataAccess;
            if (useMock) dataAccess = new Dal.Mock.Gdpr.GdprMemoryMock();
            else dataAccess = new Dal.RestServices.Gdpr.GdprCapability();
            //var serverTranslator = new ServerTranslator(dataAccess, translationService);
            //var cacheTranslator = new Cache(serverTranslator);
            //var clientTranslator = new ClientTranslator(cacheTranslator, translationService);
            // return clientTranslator;
            return dataAccess;
        }
    }
}
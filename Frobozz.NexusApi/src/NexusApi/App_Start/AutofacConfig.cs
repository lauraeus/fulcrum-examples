using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.NexusApi.Bll.Gdpr.Caches;
using Frobozz.NexusApi.Bll.Gdpr.ClientTranslators;
using Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From;
using Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.To;
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
            var gdprCapability = CreateGdprCapability();
            builder.Register(ctxt => gdprCapability)
                .As<IGdprCapability>()
                .SingleInstance();
            // Register controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Register a IDependencyResolver used by WebApi 
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        #region Capabilities
        private static IGdprCapability CreateGdprCapability(bool useMock = false)
        {
            const string clientName = "client";
            const string serverName = "server";
            var translationServiceToServer = new TranslatorServiceMock(clientName, false);
            var translationServiceFromServer = new TranslatorServiceMock(clientName, true);
            IGdprCapability dataAccess;
            if (useMock) dataAccess = new Dal.Mock.Gdpr.GdprMemoryMock();
            else dataAccess = new Dal.RestServices.Gdpr.GdprCapability();
            var serverTranslatorFrom = new ServerTranslatorFrom(dataAccess, () => serverName);
            var cacheTranslator = new Cache(serverTranslatorFrom);
            var serverTranslatorTo = new ServerTranslatorTo(cacheTranslator, () => serverName, translationServiceToServer);
            var clientTranslator = new ClientTranslator(serverTranslatorTo, () => clientName, translationServiceFromServer);
            return clientTranslator;
        }
        #endregion
    }
}
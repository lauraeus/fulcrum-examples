using Frobozz.Contracts.GdprCapability.Interfaces;
using Nexus.Link.Libraries.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ClientTranslators
{
    /// <inheritdoc />
    public class ClientTranslator : IGdprCapability
    {
        /// <inheritdoc />
        public ClientTranslator(IGdprCapability capablity, System.Func<string> getclientNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonClientTranslator(capablity, getclientNameMethod, translatorService);
            ConsentService = new ConsentClientTranslator(capablity, getclientNameMethod, translatorService);
            PersonConsentService = new PersonConsentClientTranslator(capablity, getclientNameMethod, translatorService);
            ConsentPersonService = new ConsentPersonClientTranslator(capablity, getclientNameMethod, translatorService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }

        /// <inheritdoc />
        public IConsentPersonService ConsentPersonService { get; }
    }
}
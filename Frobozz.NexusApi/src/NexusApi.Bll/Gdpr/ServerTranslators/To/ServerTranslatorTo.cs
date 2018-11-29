using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.ServerTranslators.To;
using Nexus.Link.Libraries.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.To
{
    /// <inheritdoc />
    public class ServerTranslatorTo : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorTo(IGdprCapability capablity, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            ConsentService = new ConsentServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            PersonConsentService = new PersonConsentServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            ConsentPersonService = new ConsentPersonServerTranslatorTo(capablity, getServerNameMethod, translatorService);
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
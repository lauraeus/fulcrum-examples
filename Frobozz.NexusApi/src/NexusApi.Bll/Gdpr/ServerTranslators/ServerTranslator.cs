using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.ServerTranslators;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators
{
    /// <inheritdoc />
    public class ServerTranslator : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslator(IGdprCapability capablity, ITranslatorService translatorService)
        {
            PersonService = new PersonServerTranslator(capablity, translatorService);
            ConsentService = new CrudServerTranslator<Consent>(capablity.ConsentService, "consent.id", translatorService);
            PersonConsentService = new ManyToOneServerTranslator<PersonConsent>(capablity.PersonConsentService, "person-consent.id", translatorService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
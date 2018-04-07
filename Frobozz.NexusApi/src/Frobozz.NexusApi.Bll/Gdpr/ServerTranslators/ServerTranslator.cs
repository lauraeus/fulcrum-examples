using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.NexusApi.Bll.Gdpr.ClientTranslators;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators;
using Xlent.Lever.Libraries2.MoveTo.Core.ServerTranslators;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators
{
    /// <inheritdoc />
    public class ServerTranslator : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslator(IGdprCapability capablity)
        {
            PersonService = new PersonServerTranslator(capablity);
            ConsentService = new CrudServerTranslator<Consent>(capablity.ConsentService, "consent.id");
            PersonConsentService = new ManyToOneServerTranslator<PersonConsent>(capablity.PersonConsentService, "person-consent.id");
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
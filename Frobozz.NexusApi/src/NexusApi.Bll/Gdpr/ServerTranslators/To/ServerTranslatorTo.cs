using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Core.Translation;
using Xlent.Lever.Libraries2.MoveTo.Core.Crud.ServerTranslators.To;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.To
{
    /// <inheritdoc />
    public class ServerTranslatorTo : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorTo(IGdprCapability capablity, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            ConsentService = new CrudServerTranslatorTo<Consent>(capablity.ConsentService, "consent.id", getServerNameMethod, translatorService);
            PersonConsentService = new ManyToOneServerTranslatorTo<PersonConsent>(capablity.PersonConsentService, "person-consent.id", getServerNameMethod, translatorService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
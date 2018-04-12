using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Crud.ServerTranslators.From;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <inheritdoc />
    public class ServerTranslatorFrom : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorFrom(IGdprCapability capablity, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonServerTranslatorFrom(capablity, getServerNameMethod, translatorService);
            ConsentService = new CrudServerTranslatorFrom<Consent>(capablity.ConsentService, "consent.id", getServerNameMethod, translatorService);
            PersonConsentService = new ManyToOneServerTranslatorFrom<PersonConsent>(capablity.PersonConsentService, "person-consent.id", getServerNameMethod, translatorService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
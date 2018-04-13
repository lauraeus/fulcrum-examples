using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Core.Translation;
using Xlent.Lever.Libraries2.MoveTo.Core.Crud.ClientTranslators;

namespace Frobozz.NexusApi.Bll.Gdpr.ClientTranslators
{
    /// <inheritdoc />
    public class ClientTranslator : IGdprCapability
    {
        /// <inheritdoc />
        public ClientTranslator(IGdprCapability capablity, System.Func<string> getclientNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonClientTranslator(capablity, getclientNameMethod, translatorService);
            ConsentService = new CrudClientTranslator<Consent>(capablity.ConsentService, "consent.id", getclientNameMethod, translatorService);
            PersonConsentService = new ManyToOneClientTranslator<PersonConsent>(capablity.PersonConsentService, "person-consent.id", getclientNameMethod, translatorService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
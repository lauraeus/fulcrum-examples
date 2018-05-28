using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.ClientTranslators;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ClientTranslators
{
    /// <inheritdoc />
    public class ClientTranslator : IGdprCapability
    {
        /// <inheritdoc />
        public ClientTranslator(IGdprCapability capablity, System.Func<string> getclientNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonClientTranslator(capablity, getclientNameMethod, translatorService);
            ConsentService = new CrudClientTranslator<ConsentCreate, Consent>(capablity.ConsentService, "consent.id", getclientNameMethod, translatorService);
            FulcrumAssert.IsNotNull(ConsentService);
            PersonConsentService = new SlaveToMasterClientTranslator<PersonConsentCreate, PersonConsent>(capablity.PersonConsentService, "person.id", "person-consent.id", getclientNameMethod, translatorService);
            FulcrumAssert.IsNotNull(PersonConsentService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public ICrudSlaveToMaster<PersonConsentCreate, PersonConsent, string> PersonConsentService { get; }
    }
}
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.ServerTranslators.To;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.To
{
    /// <inheritdoc />
    public class ServerTranslatorTo : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorTo(IGdprCapability capablity, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        {
            PersonService = new PersonServerTranslatorTo(capablity, getServerNameMethod, translatorService);
            ConsentService = new CrudServerTranslatorTo<ConsentCreate, Consent>(capablity.ConsentService, "consent.id", getServerNameMethod, translatorService);
            FulcrumAssert.IsNotNull(ConsentService);
            PersonConsentService = new ManyToOneServerTranslatorTo<PersonConsent>(capablity.PersonConsentService, "person-consent.id", getServerNameMethod, translatorService);
            FulcrumAssert.IsNotNull(PersonConsentService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOne<PersonConsent, string> PersonConsentService { get; }
    }
}
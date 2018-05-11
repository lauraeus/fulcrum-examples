using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.ServerTranslators.From;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <inheritdoc />
    public class ServerTranslatorFrom : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorFrom(IGdprCapability capablity, System.Func<string> getServerNameMethod)
        {
            PersonService = new PersonServerTranslatorFrom(capablity, getServerNameMethod);
            ConsentService = new CrudServerTranslatorFrom<ConsentCreate, Consent>(capablity.ConsentService, "consent.id", getServerNameMethod);
            FulcrumAssert.IsNotNull(ConsentService);
            PersonConsentService = new ManyToOneServerTranslatorFrom<PersonConsent>(capablity.PersonConsentService, "person-consent.id", getServerNameMethod);
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
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
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
            ConsentService = new CrudServerTranslatorFrom<Consent>(capablity.ConsentService, "consent.id", getServerNameMethod);
            PersonConsentService = new ManyToOneServerTranslatorFrom<PersonConsent>(capablity.PersonConsentService, "person-consent.id", getServerNameMethod);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
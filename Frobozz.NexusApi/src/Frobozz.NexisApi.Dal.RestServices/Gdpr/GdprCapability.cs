using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability
    {
        /// <inheritdoc />
        public GdprCapability()
        {
            PersonService = new PersonService("http://localhost/GdprConsent.NexusFacade.WebApi/api/Persons");
            ConsentService = new RestClientCrud<Consent, string>("http://localhost/GdprConsent.NexusFacade.WebApi/api/Consents");
            PersonConsentService = new RestClientManyToOne<PersonConsent, string>("http://localhost/GdprConsent.NexusFacade.WebApi/api/Persons", "Persons", "Consents");
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
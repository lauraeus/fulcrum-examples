using Frobozz.CapabilityContracts.Gdpr;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability
    {
        /// <inheritdoc />
        public GdprCapability()
        {
            PersonService = new PersonService("http://localhost/GdprConsent.NexusFacade.WebApi/api/Persons");
            ConsentService = new ConsentService("http://localhost/GdprConsent.NexusFacade.WebApi/api/Consents");
            PersonConsentService = new PersonConsentService("http://localhost/GdprConsent.NexusFacade.WebApi/api/Persons");
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }
    }
}
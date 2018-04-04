using Frobozz.CapabilityContracts.Gdpr;

namespace Frobozz.NexusApi.Capabilities
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability
    {
        /// <inheritdoc />
        public GdprCapability(IPersonService personService, IConsentService consentService, IPersonConsentService personConsentService)
        {
            PersonService = personService;
            ConsentService = consentService;
            PersonConsentService = personConsentService;
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }
    }
}
using Frobozz.CapabilityContracts.Gdpr;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GdprCapability(IPersonService personService, IConsentService consentService, IPersonConsentService personConsent)
        {
            PersonService = personService;
            ConsentService = consentService;
            PersonConsentService = personConsent;
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }
    }
}
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability<PersonX>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GdprCapability(IPersonService<PersonX> personService, IConsentService consentService, IPersonConsentService personConsent)
        {
            PersonService = personService;
            ConsentService = consentService;
            PersonConsentService = personConsent;
        }

        /// <inheritdoc />
        public IPersonService<PersonX> PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }
    }
}
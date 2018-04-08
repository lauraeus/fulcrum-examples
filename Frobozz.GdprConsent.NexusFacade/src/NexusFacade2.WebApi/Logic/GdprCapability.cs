using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GdprCapability(IPersonService personService, ICrud<Consent, string> consentService, IManyToOneRelation<PersonConsent, string> personConsent)
        {
            PersonService = personService;
            ConsentService = consentService;
            PersonConsentService = personConsent;
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
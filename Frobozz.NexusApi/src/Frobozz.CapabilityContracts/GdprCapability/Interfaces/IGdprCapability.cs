using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;

namespace Frobozz.Contracts.GdprCapability.Interfaces
{
    /// <summary>
    /// The services required to be a GDPR capability.
    /// </summary>
    public interface IGdprCapability 
    {
        /// <summary>
        /// Service for accessing persons.
        /// </summary>
        IPersonService PersonService { get; }

        /// <summary>
        /// Service for administrating consents
        /// </summary>
        ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <summary>
        /// Service for getting information about the consents of a specific person.
        /// </summary>
        ISlaveToMaster<PersonConsentCreate, PersonConsent, string> PersonConsentService { get; }
    }
}

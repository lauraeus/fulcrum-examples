using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Crud.Interfaces;

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
        IConsentService ConsentService { get; }

        /// <summary>
        /// Service for accessing information about the consents of a specific person.
        /// </summary>
        IPersonConsentService PersonConsentService { get; }

        /// <summary>
        /// Service for getting information about which persons that has a specific consent.
        /// </summary>
        IConsentPersonService ConsentPersonService { get; }
    }
}

using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.NexusApi.MemoryServices
{
    /// <inheritdoc cref="IPersonService" />
    public class PersonConsentMemoryStorage: MemoryManyToOnePersistance<PersonConsent, string>, IPersonConsentService
    {
        /// <inheritdoc />
        public PersonConsentMemoryStorage() : base(consent => consent.PersonId)
        {
        }
    }
}
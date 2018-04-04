using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.NexusApi.MemoryServices
{
    /// <inheritdoc cref="IConsentService" />
    public class ConsentMemoryStorage : MemoryPersistance<Consent, string>, IConsentService
    {
    }
}
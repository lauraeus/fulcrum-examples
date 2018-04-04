using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IConsentService" />
    public class ConsentMemoryStorage : MemoryPersistance<Consent, string>, IConsentService
    {
    }
}
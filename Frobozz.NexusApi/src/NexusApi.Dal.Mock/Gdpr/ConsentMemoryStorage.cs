using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.MemoryStorage;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IConsentService" />
    public class ConsentMemoryStorage : CrudMemory<ConsentCreate, Consent, string>, IConsentService
    {
    }
}
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.MemoryStorage;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IConsentPersonService" />
    public class ConsentPersonMemoryStorage : SlaveToMasterMemory<PersonConsentCreate, PersonConsent, string>, IConsentPersonService
    {
    }
}
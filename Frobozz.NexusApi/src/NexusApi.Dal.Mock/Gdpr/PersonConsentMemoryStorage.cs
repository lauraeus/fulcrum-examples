using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.MemoryStorage;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IPersonsConsentService" />
    public class PersonConsentMemoryStorage : SlaveToMasterMemory<PersonConsentCreate, PersonConsent, string>, IPersonConsentService
    {
    }
}
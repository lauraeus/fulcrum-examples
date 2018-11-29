using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Crud.MemoryStorage;
using Nexus.Link.Libraries.Core.Storage.Logic;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IPersonsConsentService" />
    public class PersonConsentMemoryStorage : SlaveToMasterMemory<PersonConsentCreate, PersonConsent, string>, IPersonConsentService
    {
    }
}
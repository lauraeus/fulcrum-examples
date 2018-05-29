using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.MemoryStorage;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IConsentPersonService" />
    public class ConsentPersonMemoryStorage : SlaveToMasterMemory<PersonConsentCreate, PersonConsent, string>, IConsentPersonService
    {
    }
}
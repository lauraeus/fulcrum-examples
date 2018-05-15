using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Helpers;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.Mappers;
using Xlent.Lever.Libraries2.Core.Crud.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers
{
    /// <inheritdoc />
    public class PersonConsentMapper : SlaveToMasterBase<PersonConsentCreate, PersonConsent, string>, ISlaveToMaster<PersonConsentCreate, PersonConsent, string>
    {
        private readonly IStorage _storage;
        /// <summary>
        /// Constructor
        /// </summary>
        public  PersonConsentMapper(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public override async Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(parentId);
            var storagePage = await _storage.PersonConsent.ReadByReference1WithPagingAsync(serverId, offset, limit, token);
            FulcrumAssert.IsNotNull(storagePage?.Data);
            var tasks = storagePage?.Data.Select(record => MapFromServerAsync(record, token));
            return new PageEnvelope<PersonConsent>(storagePage?.PageInfo, await Task.WhenAll(tasks));
        }

        /// <inheritdoc />
        public override async Task<SlaveToMasterId<string>> CreateAsync(string masterId, PersonConsentCreate item, CancellationToken token = new CancellationToken())
        {
            var serverMasterId = MapperHelper.MapToType<Guid, string>(masterId);
            var serverItem = MapToServer(item);
            var serverId = await _storage.PersonConsent.CreateAsync(serverItem, token);
            var slaveId = MapperHelper.MapToType<string, Guid>(serverId);
            return new SlaveToMasterId<string>(masterId, slaveId);
        }

        /// <inheritdoc />
        public override async Task<PersonConsent> CreateAndReturnAsync(string masterId, PersonConsentCreate item, CancellationToken token = new CancellationToken())
        {
            var serverItem = MapToServer(item);
            serverItem = await _storage.PersonConsent.CreateAndReturnAsync(serverItem, token);
            return await MapFromServerAsync(serverItem, token);
        }

        /// <inheritdoc />
        public override async Task CreateWithSpecifiedIdAsync(SlaveToMasterId<string> id, PersonConsentCreate item,
            CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var serverItem = MapToServer(item);
            await _storage.PersonConsent.CreateWithSpecifiedIdAsync(serverId.SlaveId, serverItem, token);
        }

        /// <inheritdoc />
        public override async Task<PersonConsent> CreateWithSpecifiedIdAndReturnAsync(SlaveToMasterId<string> id, PersonConsentCreate item,
            CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var serverItem = MapToServer(item);
            serverItem = await _storage.PersonConsent.CreateWithSpecifiedIdAndReturnAsync(serverId.SlaveId, serverItem, token);
            return await MapFromServerAsync(serverItem, token);
        }

        /// <inheritdoc />
        public override async Task DeleteChildrenAsync(string parentId, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(parentId);
            await _storage.PersonConsent.DeleteByReference1Async(serverId, token);
        }

        private async Task<PersonConsent> MapFromServerAsync(PersonConsentTable source,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverConsent = await _storage.Consent.ReadAsync(source.ConsentId, token);
            var target = new PersonConsent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                ConsentId = MapperHelper.MapToType<string, Guid>(source.ConsentId),
                ConsentName = serverConsent.Name,
                Etag = source.Etag,
                PersonId = MapperHelper.MapToType<string, Guid>(source.PersonId),
                HasGivenConsent = source.HasGivenConsent
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private PersonConsentTable MapToServer(PersonConsent source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsentTable
            {
                Id = MapperHelper.MapToType<Guid, string>(source.Id),
                Etag = source.Etag,
                HasGivenConsent = source.HasGivenConsent,
                PersonId = MapperHelper.MapToType<Guid, string>(source.PersonId),
                ConsentId = MapperHelper.MapToType<Guid, string>(source.ConsentId),
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private PersonConsentTable MapToServer(PersonConsentCreate source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsentTable
            {
                HasGivenConsent = source.HasGivenConsent,
                PersonId = MapperHelper.MapToType<Guid, string>(source.PersonId),
                ConsentId = MapperHelper.MapToType<Guid, string>(source.ConsentId),
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}
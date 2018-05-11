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
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic
{
    /// <inheritdoc />
    public class ConsentMapper : CrudBase<ConsentCreate, Consent, string>
    {
        private IStorage _storage;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsentMapper(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public override async Task<Consent> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var record = await _storage.Consent.ReadAsync(serverId, token);
            return MapFromServer(record);
        }

        /// <inheritdoc />
        public override async Task<PageEnvelope<Consent>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = new CancellationToken())
        {
            var storagePage = await _storage.Consent.ReadAllWithPagingAsync(offset, limit, token);
            FulcrumAssert.IsNotNull(storagePage?.Data);
            var data = storagePage?.Data.Select(MapFromServer);
            return new PageEnvelope<Consent>(storagePage?.PageInfo, data);
        }

        /// <inheritdoc />
        public override async Task<string> CreateAsync(ConsentCreate item, CancellationToken token = new CancellationToken())
        {
            var record = MapToServer(item);
            record = await _storage.Consent.CreateAndReturnAsync(record, token);
            FulcrumAssert.IsValidated(record);
            FulcrumAssert.IsNotDefaultValue(record.Id);
            return MapperHelper.MapToType<string, Guid>(record.Id);
        }

        /// <inheritdoc />
        public override async Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            await _storage.Consent.DeleteAsync(serverId, token);
        }

        /// <inheritdoc />
        public override async Task CreateWithSpecifiedIdAsync(string id, ConsentCreate item, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var record = MapToServer(item);
            await _storage.Consent.CreateWithSpecifiedIdAsync(serverId, record, token);
            FulcrumAssert.IsValidated(record);
            FulcrumAssert.IsNotDefaultValue(record.Id);
        }

        /// <inheritdoc />
        public override Task<Lock> ClaimLockAsync(string id, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            return _storage.Consent.ClaimLockAsync(serverId, token);
        }

        /// <inheritdoc />
        public override Task ReleaseLockAsync(Lock @lock, CancellationToken token = new CancellationToken())
        {
            return _storage.Consent.ReleaseLockAsync(@lock, token);
        }

        /// <inheritdoc />
        public override async Task UpdateAsync(string id, Consent item, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var record = MapToServer(item);
            await _storage.Consent.UpdateAsync(serverId, record, token);
        }

        private Consent MapFromServer(ConsentTable source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Consent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private ConsentTable MapToServer(ConsentCreate source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new ConsentTable
            {
                Name = source.Name
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private ConsentTable MapToServer(Consent source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var id = MapperHelper.MapToType<Guid, string>(source.Id);
            var target = new ConsentTable
            {
                Id = id,
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}
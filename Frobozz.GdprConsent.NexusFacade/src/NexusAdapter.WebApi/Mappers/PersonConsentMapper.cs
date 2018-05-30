using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Error.Logic;
using Xlent.Lever.Libraries2.Crud.Helpers;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.Crud.Mappers;
using Xlent.Lever.Libraries2.Crud.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers
{
    /// <inheritdoc />
    public class PersonConsentMapper : IPersonConsentService
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
        public Task CreateWithSpecifiedIdAsync(string masterId, string slaveId, PersonConsentCreate item,
            CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotNull(item, nameof(item));
            InternalContract.RequireValidated(item, nameof(item));
            InternalContract.Require(masterId == item.PersonId, $"{nameof(masterId)} ({masterId} must have the same value as {nameof(item)}.{nameof(item.PersonId)} ({item.PersonId}).");
            InternalContract.Require(slaveId == item.ConsentId, $"{nameof(slaveId)} ({slaveId} must have the same value as {nameof(item)}.{nameof(item.ConsentId)} ({item.ConsentId}).");
            var serverItem = MapToServer(item);
            return _storage.PersonConsent.CreateAsync(serverItem, token);
        }

        /// <inheritdoc />
        public async Task<PersonConsent> CreateWithSpecifiedIdAndReturnAsync(string masterId, string slaveId, PersonConsentCreate item,
            CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotNull(item, nameof(item));
            InternalContract.RequireValidated(item, nameof(item));
            InternalContract.Require(masterId == item.PersonId, $"{nameof(masterId)} ({masterId} must have the same value as {nameof(item)}.{nameof(item.PersonId)} ({item.PersonId}).");
            InternalContract.Require(slaveId == item.ConsentId, $"{nameof(slaveId)} ({slaveId} must have the same value as {nameof(item)}.{nameof(item.ConsentId)} ({item.ConsentId}).");
            var serverItem = MapToServer(item);
            serverItem = await _storage.PersonConsent.CreateAndReturnAsync(serverItem, token);
            return await MapFromServerAsync(serverItem, token);
        }

        /// <inheritdoc />
        public async Task<PersonConsent> ReadAsync(string masterId, string slaveId, CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotDefaultValue(masterId, nameof(masterId));
            InternalContract.RequireNotDefaultValue(slaveId, nameof(slaveId));
            var serverMasterId = MapperHelper.MapToType<Guid, string>(masterId);
            var serverSlaveId = MapperHelper.MapToType<Guid, string>(slaveId);
            var serverItem = await _storage.PersonConsent.ReadAsync(serverMasterId, serverSlaveId, token);
            return await MapFromServerAsync(serverItem, token);
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string personId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(personId);
            var storagePage = await _storage.PersonConsent.ReadByReference1WithPagingAsync(serverId, offset, limit, token);
            FulcrumAssert.IsNotNull(storagePage?.Data);
            var tasks = storagePage?.Data.Select(record => MapFromServerAsync(record, token));
            return new PageEnvelope<PersonConsent>(storagePage?.PageInfo, await Task.WhenAll(tasks));
        }

        /// <inheritdoc />
        public Task UpdateAsync(string masterId, string slaveId, PersonConsent item,
            CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotDefaultValue(masterId, nameof(masterId));
            InternalContract.RequireNotDefaultValue(slaveId, nameof(slaveId));
            var serverMasterId = MapperHelper.MapToType<Guid, string>(masterId);
            var serverSlaveId = MapperHelper.MapToType<Guid, string>(slaveId);
            var serverItem = MapToServer(item);
            return _storage.PersonConsent.UpdateAsync(serverMasterId, serverSlaveId, serverItem, token);
        }

        /// <inheritdoc />
        public Task DeleteAsync(string masterId, string slaveId, CancellationToken token = new CancellationToken())
        {
            InternalContract.RequireNotDefaultValue(masterId, nameof(masterId));
            InternalContract.RequireNotDefaultValue(slaveId, nameof(slaveId));
            var serverMasterId = MapperHelper.MapToType<Guid, string>(masterId);
            var serverSlaveId = MapperHelper.MapToType<Guid, string>(slaveId);
            return _storage.PersonConsent.DeleteAsync(serverMasterId, serverSlaveId, token);
        }

        /// <inheritdoc />
        public async Task DeleteChildrenAsync(string parentId, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(parentId);
            await _storage.PersonConsent.DeleteByReference1Async(serverId, token);
        }

        private async Task<PersonConsent> MapFromServerAsync(PersonConsentTable source,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverConsentTask = _storage.Consent.ReadAsync(source.ConsentId, token);
            var serverPersonTask = _storage.Consent.ReadAsync(source.PersonId, token);
            var serverConsent = await serverConsentTask;
            var serverPerson = await serverPersonTask;
            if (serverConsent == null) throw new FulcrumNotFoundException($"Could not find consent with id {source.ConsentId}");
            if (serverPerson == null) throw new FulcrumNotFoundException($"Could not find person with id {source.PersonId}");
            var target = new PersonConsent
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                ConsentId = MapperHelper.MapToType<string, Guid>(source.ConsentId),
                PersonName = serverPerson.Name,
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
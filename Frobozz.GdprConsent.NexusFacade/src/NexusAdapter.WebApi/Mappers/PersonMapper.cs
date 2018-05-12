using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Helpers;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.Mappers;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers
{
    /// <inheritdoc cref="IPersonService" />
    public class PersonMapper : CrudBase<PersonCreate, Person, string>, IPersonService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor
        /// </summary>
        public PersonMapper(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public override async Task<Person> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var record = await _storage.Person.ReadAsync(serverId, token);
            return await MapFromServerAsync(record, token);
        }

        /// <inheritdoc />
        public override async Task<PageEnvelope<Person>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = new CancellationToken())
        {
            var storagePage = await _storage.Person.ReadAllWithPagingAsync(offset, limit, token);
            FulcrumAssert.IsNotNull(storagePage?.Data);
            var tasks = storagePage?.Data.Select(record => MapFromServerAsync(record, token));
            return new PageEnvelope<Person>(storagePage?.PageInfo, await Task.WhenAll(tasks));
        }

        /// <inheritdoc />
        public override async Task<string> CreateAsync(PersonCreate item, CancellationToken token = new CancellationToken())
        {
            var record = await MapToServerAsync(item, token);
            record = await _storage.Person.CreateAndReturnAsync(record, token);
            FulcrumAssert.IsValidated(record);
            FulcrumAssert.IsNotDefaultValue(record.Id);
            await UpdateAddressesAsync(record.Id, item, token);
            return MapperHelper.MapToType<string, Guid>(record.Id);
        }

        /// <inheritdoc />
        public override async Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var item = new PersonCreate();
            await UpdateAddressesAsync(serverId, item, token);
            await _storage.Person.DeleteAsync(serverId, token);
        }

        /// <inheritdoc />
        public override async Task CreateWithSpecifiedIdAsync(string id, PersonCreate item, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var record = await MapToServerAsync(item, token);
            await _storage.Person.CreateWithSpecifiedIdAsync(serverId, record, token);
            FulcrumAssert.IsValidated(record);
            FulcrumAssert.IsNotDefaultValue(record.Id);
            await UpdateAddressesAsync(record.Id, item, token);
        }

        /// <inheritdoc />
        public override Task<Lock> ClaimLockAsync(string id, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            return _storage.Person.ClaimLockAsync(serverId, token);
        }

        /// <inheritdoc />
        public override Task ReleaseLockAsync(Lock @lock, CancellationToken token = new CancellationToken())
        {
            return _storage.Person.ReleaseLockAsync(@lock, token);
        }

        /// <inheritdoc />
        public override async Task UpdateAsync(string id, Person item, CancellationToken token = new CancellationToken())
        {
            var serverId = MapperHelper.MapToType<Guid, string>(id);
            var record = await MapToServerAsync(item, token);
            await _storage.Person.UpdateAsync(serverId, record, token);
            await UpdateAddressesAsync(record.Id, item, token);
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var enumerator = new PageEnvelopeEnumeratorAsync<PersonTable>((o, t) => _storage.Person.ReadAllWithPagingAsync(o, null, t), token);
            while (await enumerator.MoveNextAsync())
            {
                if (enumerator.Current.Name == name) return await MapFromServerAsync(enumerator.Current, token);
            }
            return null;
        }

        private async Task<Person> MapFromServerAsync(PersonTable source,
            CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var serverAddresses = await _storage.Address.ReadChildrenAsync(source.Id, token: token);
            var target = new Person
            {
                Id = MapperHelper.MapToType<string, Guid>(source.Id),
                Name = source.Name,
                Etag = source.Etag,
                Addresses = serverAddresses.Select(CreateAndMapFromServer)
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private async Task<PersonTable> MapToServerAsync(PersonCreate source, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonTable
            {
                Name = source.Name
            };
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }

        private async Task<PersonTable> MapToServerAsync(Person source, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonTable
            {
                Id = MapperHelper.MapToType<Guid, string>(source.Id),
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return await Task.FromResult(target);
        }

        private async Task UpdateAddressesAsync(Guid personId, PersonCreate person, CancellationToken token)
        {
            var serverAddresses = await _storage.Address.ReadChildrenAsync(personId, token: token);
            var serverAddressArray = serverAddresses as AddressTable[] ?? serverAddresses.ToArray();
            var tasks = new List<Task>();
            for (var typeInt = 1; typeInt < 5; typeInt++)
            {
                var typeString = MapperHelper.MapToType<string, int>(typeInt);
                var serverAddress = serverAddressArray.FirstOrDefault(a => a.Type == typeInt);
                var address = person.Addresses.FirstOrDefault(a => a.Type == typeString);
                if (serverAddress == null)
                {
                    if (address == null) continue;
                    serverAddress = ToServer(personId, address);
                    var task = _storage.Address.CreateAsync(serverAddress, token);
                    tasks.Add(task);
                }
                else
                {
                    Task task;
                    if (address == null)
                    {
                        task = _storage.Address.DeleteAsync(serverAddress.Id, token);
                    }
                    else
                    {
                        var updatedServerAddress = ToServer(personId, address);
                        updatedServerAddress.Id = serverAddress.Id;
                        updatedServerAddress.Etag = serverAddress.Etag;
                        task = _storage.Address.UpdateAsync(updatedServerAddress.Id, updatedServerAddress, token);
                    }
                    tasks.Add(task);
                }
            }
            await Task.WhenAll(tasks);
        }

        private AddressTable ToServer(Guid personId, Address source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new AddressTable
            {
                Type = MapperHelper.MapToType<int, string>(source.Type),
                Street = source.Street,
                City = source.City,
                PersonId = personId
            };
            return target;
        }

        private Address CreateAndMapFromServer(AddressTable source)
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Address
            {
                Type = MapperHelper.MapToType<string,int>(source.Type),
                Street = source.Street,
                City = source.City
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}
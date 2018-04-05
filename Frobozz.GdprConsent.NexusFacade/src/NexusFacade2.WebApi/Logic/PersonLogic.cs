﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class PersonLogic : CrudMapper<Person, string, IStorage, PersonTable, Guid>, IPersonService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonLogic(IStorage storage)
        :base(storage.Person, storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<Person> CreateAndReturnAsync(Person item, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            var personDb = ToDb(item);
            personDb = await _storage.Person.CreateAndReturnAsync(personDb, token);
            await CreateAddressesAsync(personDb.Id, item, token);
            return await ToServiceAsync(personDb, token: token);
        }

        private async Task CreateAddressesAsync(Guid personId, Person item, CancellationToken token = default(CancellationToken))
        {
            var tasks = new List<Task<Guid>>();
            foreach (var address in item.Addresses)
            {
                var addressDb = ToDb(personId, address);
                var task = _storage.Address.CreateAsync(addressDb, token);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }

        /// <inheritdoc />
        public async Task<Person> UpdateAndReturnAsync(string id, Person item, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            var personDb = ToDb(item);
            personDb = await _storage.Person.UpdateAndReturnAsync(personDb.Id, personDb, token);
            await UpdateAddressesAsync(personDb.Id, item, token);
            return await ToServiceAsync(personDb, token: token);
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNullOrWhitespace(name, nameof(name));
            var enumerator =
                new PageEnvelopeEnumeratorAsync<PersonTable>((offset, t) => _storage.Person.ReadAllWithPagingAsync(offset, null, t), token);
            while (await enumerator.MoveNextAsync())
            {
                if (enumerator.Current.Name == name) return await ToServiceAsync(enumerator.Current, token: token);
            }

            return default(Person);
        }

        private async Task UpdateAddressesAsync(Guid personId, Person person, CancellationToken token)
        {
            var addressesDb = await _storage.Address.ReadChildrenAsync(personId, token: token);
            var addressTables = addressesDb as AddressTable[] ?? addressesDb.ToArray();
            var tasks = new List<Task>();
            for (var typeInt = 1; typeInt < 5; typeInt++)
            {
                var typeString = ToAddressTypeService(typeInt);
                var addressDb = addressTables.FirstOrDefault(a => a.Type == typeInt);
                var address = person.Addresses.FirstOrDefault(a => a.Type == typeString);
                if (addressDb == null)
                {
                    if (address == null) continue;
                    addressDb = ToDb(personId, address);
                    var task = _storage.Address.CreateAsync(addressDb, token);
                    tasks.Add(task);
                }
                else
                {
                    Task task;
                    if (address == null)
                    {
                        task = _storage.Address.DeleteAsync(addressDb.Id, token);
                    }
                    else
                    {
                        var updatedAddressDb = ToDb(personId, address);
                        updatedAddressDb.Id = addressDb.Id;
                        updatedAddressDb.Etag = addressDb.Etag;
                        task = _storage.Address.UpdateAsync(updatedAddressDb.Id, updatedAddressDb, token);
                    }
                    tasks.Add(task);
                }
            }
            await Task.WhenAll(tasks);
        }

        private Address ToService(AddressTable source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Address
            {
                Type = ToAddressTypeService(source.Type),
                Street = source.Street,
                City = source.City
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private AddressTable ToDb(Guid personId, Address source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new AddressTable
            {
                Type = ToAddressTypeDb(source.Type),
                Street = source.Street,
                City = source.City,
                PersonId = personId
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private static int ToAddressTypeDb(string source)
        {
            switch (source)
            {
                case "Public": return 1;
                case "Invoice": return 2;
                case "Delivery": return 3;
                case "Postal": return 4;
                default:
                    FulcrumAssert.Fail($"Unknown address type ({source}).");
                    return 0;
            }
        }

        private static string ToAddressTypeService(int source)
        {
            switch (source)
            {
                case 1: return "Public";
                case 2: return "Invoice";
                case 3:
                    return "Delivery";
                case 4: return "Postal";
                default:
                    FulcrumAssert.Fail($"Unknown address type ({source}).");
                    return "None";
            }
        }

        private async Task<Person> ToServiceAsync(PersonTable source, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var addressesDbTask = _storage.Address.ReadChildrenAsync(source.Id, token: token);
            var target = new Person()
            {
                Id = source.Id.ToString(),
                Name = source.Name,
                Etag = source.Etag,
                Addresses = (await addressesDbTask).Select(db => ToService(db))
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private PersonTable ToDb(Person source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var id = new Guid(source.Id);
            var target = new PersonTable()
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

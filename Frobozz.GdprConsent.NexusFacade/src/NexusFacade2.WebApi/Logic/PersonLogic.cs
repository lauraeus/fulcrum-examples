using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Error.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;
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
        public async Task<Person> CreateAndReturnAsync(Person item)
        {
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            var personDb = ToDb(item);
            personDb = await _storage.Person.CreateAndReturnAsync(personDb);
            await CreateAddressesAsync(personDb.Id, item);
            return await ToServiceAsync(personDb);
        }

        private async Task CreateAddressesAsync(Guid personId, Person item)
        {
            var tasks = new List<Task<Guid>>();
            foreach (var address in item.Addresses)
            {
                var addressDb = ToDb(personId, address);
                var task = _storage.Address.CreateAsync(addressDb);
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }

        /// <inheritdoc />
        public async Task<Person> UpdateAndReturnAsync(string id, Person item)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            var personDb = ToDb(item);
            personDb = await _storage.Person.UpdateAndReturnAsync(personDb.Id, personDb);
            await UpdateAddressesAsync(personDb.Id, item);
            return await ToServiceAsync(personDb);
        }

        private async Task UpdateAddressesAsync(Guid personId, Person person)
        {
            var addressesDb = await _storage.Address.ReadChildrenAsync(personId);
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
                    var task = _storage.Address.CreateAsync(addressDb);
                    tasks.Add(task);
                }
                else
                {
                    Task task;
                    if (address == null)
                    {
                        task = _storage.Address.DeleteAsync(addressDb.Id);
                    }
                    else
                    {
                        var updatedAddressDb = ToDb(personId, address);
                        updatedAddressDb.Id = addressDb.Id;
                        updatedAddressDb.Etag = addressDb.Etag;
                        task = _storage.Address.UpdateAsync(updatedAddressDb.Id, updatedAddressDb);
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

        private async Task<Person> ToServiceAsync(PersonTable source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var addressesDbTask = _storage.Address.ReadChildrenAsync(source.Id);
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

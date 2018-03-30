using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Error.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using AddressTypeEnum = Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel.AddressTypeEnum;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons")]
    public partial class PersonsController : ApiController
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonsController(IStorage storage)
        {
            _storage = storage;
        }

        private Address ToService(AddressTable source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Address
            {
                Type = source.Type.ToString(),
                Street = source.Street,
                City = source.City
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }

        private Consent ToService(ConsentTable source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Consent
            {
                Name = source.Name
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
                case 1: return AddressTypeEnum.Public.ToString();
                case 2:  return AddressTypeEnum.Invoice.ToString();
                case 3:
                    return AddressTypeEnum.Delivery.ToString();
                case 4: return AddressTypeEnum.Postal.ToString();
                default:
                    FulcrumAssert.Fail($"Unknown address type ({source}).");
                    return AddressTypeEnum.None.ToString();
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

    public partial class PersonsController : IReadAll<Person, string>
    {
        /// <inheritdoc />
        [HttpGet]
        [Route("{id}")]
        public async Task<Person> ReadAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            var parentId = new Guid(id);
            var personDb = await _storage.Person.ReadAsync(parentId);
            return await ToServiceAsync(personDb);
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<Person>> ReadAllWithPagingAsync(int offset, int? limit = null)
        {
            ServiceContract.RequireGreaterThanOrEqualTo(0, offset, nameof(offset));
            if (limit != null)
            {
                ServiceContract.RequireGreaterThan(0, limit.Value, nameof(limit));
            }

            var personsDb = await _storage.Person.ReadAllWithPagingAsync(offset, limit);
            var persons = personsDb.Data
                .Select(async p => await ToServiceAsync(p));
            return new PageEnvelope<Person>(personsDb.PageInfo, await Task.WhenAll(persons));
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Person>> ReadAllAsync(int limit = 2147483647)
        {
            return await StorageHelper.ReadPages(offset => ReadAllWithPagingAsync(offset));
        }
    }

    public partial class PersonsController : ICrd<Person, string>
    {
        /// <inheritdoc />
        public Task<string> CreateAsync(Person item)
        {
            throw new FulcrumNotImplementedException();
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("")]
        public async Task<Person> CreateAndReturnAsync(Person item)
        {
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            var personDb = ToDb(item);
            personDb = await _storage.Person.CreateAndReturnAsync(personDb);
            await CreateAddressesAsync(personDb.Id, item);
            return await ToServiceAsync(personDb);
        }

        /// <inheritdoc />
        public Task CreateWithSpecifiedIdAsync(string id, Person item)
        {
            throw new FulcrumNotImplementedException();
        }

        /// <inheritdoc />
        public Task<Person> CreateWithSpecifiedIdAndReturnAsync(string id, Person item)
        {
            throw new FulcrumNotImplementedException();
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            await _storage.Person.DeleteAsync(new Guid(id));
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("")]
        public async Task DeleteAllAsync()
        {
            await _storage.Person.DeleteAllAsync();
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
    }

    public partial class PersonsController : ICrud<Person, string>
    {
        /// <inheritdoc />
        public Task UpdateAsync(string id, Person item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("{id}")]
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
    }

    public partial class PersonsController : IManyToOneRelation<Consent, string>
    {
        /// <inheritdoc />
        public Task<PageEnvelope<Consent>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}/Consents")]
        public async Task<IEnumerable<Consent>> ReadChildrenAsync(string personId, int limit = int.MaxValue)
        {
            var id = new Guid(personId);
            var consentsDb = await _storage.PersonConsent.R(id, limit);
            return consentsDb.Select(c => ToService(c));
        }

        /// <inheritdoc />
        public async Task<Person> ReadParentAsync(string childId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task DeleteChildrenAsync(string parentId)
        {
            throw new NotImplementedException();
        }
    }
}

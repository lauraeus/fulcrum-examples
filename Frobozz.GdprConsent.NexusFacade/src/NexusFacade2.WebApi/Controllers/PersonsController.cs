using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.DalModel;
using Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons")]
    public class PersonsController : ApiController, IRead<Person, string>
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonsController(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public async Task<Person> ReadAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            var parentId = new Guid(id);
            var readTask = _storage.Person.ReadAsync(parentId);
            var addressesDb = await _storage.Address.ReadChildrenAsync(parentId);
            var personDb = await readTask;
            var person = ToService(personDb);
            person.Addresses = addressesDb.Select(db => ToService(db));
            return person;
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

        private Person ToService(PersonTable source, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new Person()
            {
                Id = source.Id.ToString(),
                Name = source.Name,
                Etag = source.Etag
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}

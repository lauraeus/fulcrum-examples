using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.ApiHelpers;
using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.NexusApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons")]
    public partial class PersonsController : ApiController
    {
        private ICrud<Person, string> _apiHelper;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonsController(IGdprCapability gdprCapability)
        {
            _apiHelper = new ApiCrudHelper<Person>(gdprCapability.Person);
        }
    }

    public partial class PersonsController : ICrud<Person, string>
    {
        /// <inheritdoc />
        [HttpPost]
        [Route("")]
        public async Task<string> CreateAsync(Person item)
        {
            return await _apiHelper.CreateAsync(item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("ReturnCreated")]
        public async Task<Person> CreateAndReturnAsync(Person item)
        {
            return await _apiHelper.CreateAndReturnAsync(item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}")]
        public async Task CreateWithSpecifiedIdAsync(string id, Person item)
        {
            await _apiHelper.CreateWithSpecifiedIdAsync(id, item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/ReturnCreated")]
        public async Task<Person> CreateWithSpecifiedIdAndReturnAsync(string id, Person item)
        {
            return await _apiHelper.CreateWithSpecifiedIdAndReturnAsync(id, item);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}")]
        public async Task<Person> ReadAsync(string id)
        {
            return await _apiHelper.ReadAsync(id);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("WithPaging")]
        public async Task<PageEnvelope<Person>> ReadAllWithPagingAsync(int offset, int? limit = null)
        {
            return await _apiHelper.ReadAllWithPagingAsync(offset, limit);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Person>> ReadAllAsync(int limit = int.MaxValue)
        {
            return await _apiHelper.ReadAllAsync(limit);
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _apiHelper.DeleteAsync(id);
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("")]
        public async Task DeleteAllAsync()
        {
            await _apiHelper.DeleteAllAsync();
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(string id, Person item)
        {
            await _apiHelper.UpdateAsync(id, item);
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("{id}/ReturnUpdated")]
        public async Task<Person> UpdateAndReturnAsync(string id, Person item)
        {
            return await _apiHelper.UpdateAndReturnAsync(id, item);
        }
    }
}

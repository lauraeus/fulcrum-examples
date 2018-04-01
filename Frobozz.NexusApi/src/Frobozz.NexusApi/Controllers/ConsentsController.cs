using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.NexusApi.Helpers;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.NexusApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Consents")]
    public partial class ConsentsController : ApiController
    {
        private ICrud<Consent, string> _apiHelper;

        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentsController(IGdprCapability gdprCapability)
        {
            _apiHelper = new ApiCrudHelper<Consent>(gdprCapability.Consent);
        }
    }

    public partial class ConsentsController : ICrud<Consent, string>
    {
        /// <inheritdoc />
        [HttpPost]
        [Route("")]
        public async Task<string> CreateAsync(Consent item)
        {
            return await _apiHelper.CreateAsync(item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("ReturnCreated")]
        public async Task<Consent> CreateAndReturnAsync(Consent item)
        {
            return await _apiHelper.CreateAndReturnAsync(item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}")]
        public async Task CreateWithSpecifiedIdAsync(string id, Consent item)
        {
            await _apiHelper.CreateWithSpecifiedIdAsync(id, item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/ReturnCreated")]
        public async Task<Consent> CreateWithSpecifiedIdAndReturnAsync(string id, Consent item)
        {
            return await _apiHelper.CreateWithSpecifiedIdAndReturnAsync(id, item);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}")]
        public async Task<Consent> ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("WithPaging")]
        public async Task<PageEnvelope<Consent>> ReadAllWithPagingAsync(int offset, int? limit = null)
        {
            return await _apiHelper.ReadAllWithPagingAsync(offset, limit);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Consent>> ReadAllAsync(int limit = int.MaxValue)
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
        public async Task UpdateAsync(string id, Consent item)
        {
            await _apiHelper.UpdateAsync(id, item);
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("{id}/ReturnUpdated")]
        public async Task<Consent> UpdateAndReturnAsync(string id, Consent item)
        {
            return await _apiHelper.UpdateAndReturnAsync(id, item);
        }
    }
}

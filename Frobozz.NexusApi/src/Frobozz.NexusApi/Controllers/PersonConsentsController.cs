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
    [RoutePrefix("api/Persons/{parentId}/Consents")]
    public partial class PersonConsentsController : ApiController
    {
        private readonly IManyToOneRelation<Consent, string> _apiHelper;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentsController(IGdprCapability gdprCapability)
        {
            _apiHelper = new ApiManyToOneHelper<Consent>(gdprCapability.PersonConsent);
        }
    }

    public partial class PersonConsentsController : IManyToOneRelation<Consent, string>
    {
        /// <inheritdoc />
        [HttpGet]
        [Route("WithPaging")]
        public async Task<PageEnvelope<Consent>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null)
        {
            return await _apiHelper.ReadChildrenWithPagingAsync(parentId, offset, limit);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Consent>> ReadChildrenAsync(string parentId, int limit = int.MaxValue)
        {
            return await _apiHelper.ReadChildrenAsync(parentId, limit);
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("")]
        public async Task DeleteChildrenAsync(string parentId)
        {
            await _apiHelper.DeleteChildrenAsync(parentId);
        }
    }
}

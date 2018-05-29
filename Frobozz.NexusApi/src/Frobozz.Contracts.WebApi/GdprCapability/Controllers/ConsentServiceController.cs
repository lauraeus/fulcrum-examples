using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.WebApi.Annotations;
using Xlent.Lever.Libraries2.WebApi.Crud.ApiControllers;

namespace Frobozz.Contracts.WebApi.GdprCapability.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("Gdpr/Consents")]
    public abstract class ConsentServiceController : CrudApiController<ConsentCreate, Consent>, IConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        protected ConsentServiceController(IGdprCapability gdprCapability)
            : base(gdprCapability.ConsentService)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a new consent
        /// </summary>
        /// <param name="item">The data for the consent to create.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created consent.</returns>
        [HttpPost]
        [Route("")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse(typeof(string))]
        public override Task<string> CreateAsync(ConsentCreate item, CancellationToken token = new CancellationToken())
        {
            return base.CreateAsync(item, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the consent with the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The id of the consent to get.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found consent or null.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse(typeof(Consent))]
        public override Task<Consent> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.ReadAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get all consents
        /// </summary>
        /// <param name="offset">The number of items that will be skipped in result.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found consent or null.</returns>
        [HttpGet]
        [Route("")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse(typeof(PageEnvelope<Consent>))]
        public override Task<PageEnvelope<Consent>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = new CancellationToken())
        {
            return base.ReadAllWithPagingAsync(offset, limit, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a new consent
        /// </summary>
        /// <param name="id">The id of the consent to update.</param>
        /// <param name="item">The updated information for the consent.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created consent.</returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerGroup("Consents")]
        public override Task UpdateAsync(string id, Consent item, CancellationToken token = new CancellationToken())
        {
            return base.UpdateAsync(id, item, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete a consent
        /// </summary>
        /// <param name="id">The id of the consent to delete.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created consent.</returns>
        [HttpDelete]
        [Route("{id}")]
        [SwaggerGroup("Consents")]
        public override Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.DeleteAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete all consents
        /// </summary>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created consent.</returns>
        [HttpDelete]
        [Route("")]
        [SwaggerGroup("Consents")]
        public override Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            return base.DeleteAllAsync(token);
        }
    }
}

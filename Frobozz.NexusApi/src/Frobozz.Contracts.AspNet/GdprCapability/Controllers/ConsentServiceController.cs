using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.AspNet.Controllers;
using Nexus.Link.Libraries.Web.AspNet.Annotations;
#if NETCOREAPP
using Microsoft.AspNetCore.Mvc;
#elif NET472
using System.Web.Http;
#endif

namespace Frobozz.Contracts.AspNet.GdprCapability.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    public abstract class ConsentServiceController : CrudController<ConsentCreate, Consent>, IConsentService
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
        /// Create a consent
        /// </summary>
        /// <param name="item">The data for the consent to create.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created consent.</returns>
        [HttpPost]
        [Route("Gdpr/Consents")]
        [SwaggerGroup("Gdpr/Consents")]
        [SwaggerSuccessResponse(typeof(string))]
        public override Task<string> CreateAsync(ConsentCreate item, CancellationToken token = new CancellationToken())
        {
            return base.CreateAsync(item, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a specific consent.
        /// </summary>
        /// <param name="id">The id of the consent to get.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found consent or null.</returns>
        [HttpGet]
        [Route("Gdpr/Consents/{id}")]
        [SwaggerGroup("Gdpr/Consents")]
        [SwaggerSuccessResponse(typeof(Consent))]
        public override Task<Consent> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.ReadAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a page of consents
        /// </summary>
        /// <param name="offset">The number of items that will be skipped in result.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found consent or null.</returns>
        [HttpGet]
        [Route("Gdpr/Consents")]
        [SwaggerGroup("Gdpr/Consents")]
        [SwaggerSuccessResponse(typeof(PageEnvelope<Consent>))]
        public override Task<PageEnvelope<Consent>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = new CancellationToken())
        {
            return base.ReadAllWithPagingAsync(offset, limit, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Update a consent
        /// </summary>
        /// <param name="id">The id of the consent to update.</param>
        /// <param name="item">The updated information for the consent.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created consent.</returns>
        [HttpPut]
        [Route("Gdpr/Consents/{id}")]
        [SwaggerGroup("Gdpr/Consents")]
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
        [SwaggerGroup("Gdpr/Consents")]
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
        [Route("Gdpr/Consents")]
        [SwaggerGroup("Gdpr/Consents")]
        public override Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            return base.DeleteAllAsync(token);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.AspNet.Controllers;
using Nexus.Link.Libraries.Web.AspNet.Annotations;

namespace Frobozz.Contracts.WebApi.GdprCapability.Controllers
{
    /// <inheritdoc cref="IConsentPersonService" />
    public abstract class ConsentPersonServiceController : CrudSlaveToMasterController<PersonConsentCreate, PersonConsent>, IConsentPersonService
    {
        /// <inheritdoc />
        protected ConsentPersonServiceController(IGdprCapability gdprCapability)
            : base(gdprCapability.ConsentPersonService)
        {
        }

        /// <summary>
        /// Get a page of persons for a specific consent..
        /// </summary>
        /// <param name="consentId">The id for the consent to find persons.</param>
        /// <param name="offset">How many items to skip before crating the page.</param>
        /// <param name="limit">The maaximum number of items in the page. Null means use the greatest number that is efficient for the implementation.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>A <see cref="PageEnvelope{T}"/> of persons that has a record for the specified consent. .</returns>
        [HttpGet]
        [Route("Gdpr/Consents/{consentId}/Persons")]
        [SwaggerGroup("Gdpr/Consents")]
        [SwaggerSuccessResponse(typeof(PageEnvelope<PersonConsent>))]
        public override Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string consentId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            return base.ReadChildrenWithPagingAsync(consentId, offset, limit, token);
        }
    }
}

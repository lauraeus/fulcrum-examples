using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Crud.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.WebApi.Annotations;
using Xlent.Lever.Libraries2.WebApi.Crud.ApiControllers;

namespace Frobozz.Contracts.WebApi.GdprCapability.Controllers
{
    /// <inheritdoc cref="IConsentPersonService" />
    [RoutePrefix("Gdpr/Consents/{consentId}/Persons")]
    public abstract class ConsentPersonServiceController : SlaveToMasterApiController<PersonConsentCreate, PersonConsent>, IConsentPersonService
    {
        /// <inheritdoc />
        protected ConsentPersonServiceController(IGdprCapability gdprCapability)
            : base(gdprCapability.ConsentPersonService)
        {
        }

        /// <summary>
        /// Get a page of consents for the specified person.
        /// </summary>
        /// <param name="consentId">The id for the person to find consents for.</param>
        /// <param name="offset">How many items to skip before crating the page.</param>
        /// <param name="limit">The maaximum number of items in the page. Null means use the greatest number that is efficient for the implementation.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>A <see cref="PageEnvelope{T}"/> of consents that belongs to the specified person.</returns>
        [HttpGet]
        [Route("")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse(typeof(PageEnvelope<PersonConsent>))]
        public override Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string consentId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            return base.ReadChildrenWithPagingAsync(consentId, offset, limit, token);
        }
    }
}

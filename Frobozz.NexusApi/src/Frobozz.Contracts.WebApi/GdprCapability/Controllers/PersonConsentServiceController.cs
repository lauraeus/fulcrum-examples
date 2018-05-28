using System;
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
    /// <inheritdoc />
    public abstract class PersonConsentServiceController : SlaveToMasterApiController<PersonConsentCreate, PersonConsent>
    {
        /// <inheritdoc />
        public PersonConsentServiceController(IGdprCapability gdprCapability)
            : base(gdprCapability.PersonConsentService)
        {
        }

        /// <summary>
        /// Get a page of consents for the specified person.
        /// </summary>
        /// <param name="personId">The id for the person to find consents for.</param>
        /// <param name="offset">How many items to skip before crating the page.</param>
        /// <param name="limit">The maaximum number of items in the page. Null means use the greatest number that is efficient for the implementation.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>A <see cref="PageEnvelope{T}"/> of consents that belongs to the specified person.</returns>
        [HttpGet]
        [Route("Gdpr/Persons/{personId}/Consents")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(PageEnvelope<PersonConsent>))]
        public override Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string personId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            return base.ReadChildrenWithPagingAsync(personId, offset, limit, token);
        }

        /// <summary>
        /// Add a new consent to a person
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="item"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Gdpr/Persons/{personId}/Consents")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(string))]
        public override Task<string> CreateAsync(string personId, PersonConsentCreate item, CancellationToken token = new CancellationToken())
        {
            return base.CreateAsync(personId, item, token);
        }
    }
}

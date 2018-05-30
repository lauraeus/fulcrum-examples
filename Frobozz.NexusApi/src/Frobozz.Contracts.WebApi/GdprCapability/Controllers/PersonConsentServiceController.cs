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
    [RoutePrefix("Gdpr/Persons/{personId}/Consents")]
    public abstract class PersonConsentServiceController : 
        SlaveToMasterApiController<PersonConsentCreate, PersonConsent>,
        IPersonConsentService
    {
        /// <inheritdoc />
        protected PersonConsentServiceController(IGdprCapability gdprCapability)
            : base(gdprCapability.PersonConsentService)
        {
        }

        /// <summary>
        /// Add a new consent to a person
        /// </summary>
        /// <param name="personId">The id for the person to create the person consent for.</param>
        /// <param name="consentId">The id for the consent to update the person consent for.</param>
        /// <param name="item">The information for the new person consent</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        [HttpPost]
        [Route("{consentId}")]
        [SwaggerGroup("Persons")]
        public override Task CreateWithSpecifiedIdAsync(string personId, string consentId, PersonConsentCreate item, CancellationToken token = new CancellationToken())
        {
            return base.CreateAsync(personId, item, token);
        }

        /// <summary>
        /// Get a page of consents for the specified person.
        /// </summary>
        /// <param name="personId">The id for the person to find the person consent for.</param>
        /// <param name="consentId">The id for the consent to find the person consent for.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The specified consent for the specified person.</returns>
        [HttpGet]
        [Route("{consentId}")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(PersonConsent))]
        public override Task<PersonConsent> ReadAsync(string personId, string consentId, CancellationToken token = new CancellationToken())
        {
            return base.ReadAsync(personId, consentId, token);
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
        [Route("")]
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
        /// <param name="personId">The id for the person to create the person consent for.</param>
        /// <param name="consentId">The id for the consent to update the person consent for.</param>
        /// <param name="item">The information for the new person consent</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        [HttpPut]
        [Route("{consentId}")]
        [SwaggerGroup("Persons")]
        public override Task UpdateAsync(string personId, string consentId, PersonConsent item,
            CancellationToken token = new CancellationToken())
        {
            return base.UpdateAsync(personId, consentId, item, token);
        }

        /// <summary>
        /// Get a page of consents for the specified person.
        /// </summary>
        /// <param name="personId">The id for the person to delete the person consent for.</param>
        /// <param name="consentId">The id for the consent to delete the person consent for.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The specified consent for the specified person.</returns>
        [HttpDelete]
        [Route("{consentId}")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(PersonConsent))]
        public override Task DeleteAsync(string personId, string consentId, CancellationToken token = new CancellationToken())
        {
            return base.DeleteAsync(personId, consentId, token);
        }

        /// <summary>
        /// Get a page of consents for the specified person.
        /// </summary>
        /// <param name="personId">The id for the person to delete all the person consents for.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The specified consent for the specified person.</returns>
        [HttpDelete]
        [Route("")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(PersonConsent))]
        public override Task DeleteChildrenAsync(string personId, CancellationToken token = new CancellationToken())
        {
            return base.DeleteChildrenAsync(personId, token);
        }
    }
}

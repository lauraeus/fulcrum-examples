using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.WebApi.Annotations;
using Xlent.Lever.Libraries2.WebApi.Crud.ApiControllers;

namespace Frobozz.Contracts.WebApi.GdprCapability.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    public abstract class ConsentServiceController : CrudApiController<ConsentCreate, Consent>
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentServiceController(IGdprCapability gdprCapability)
            : base(gdprCapability.ConsentService)
        {
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("Gdpr/Consents")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse(typeof(string))]
        public override Task<string> CreateAsync(ConsentCreate item, CancellationToken token = new CancellationToken())
        {
            return base.CreateAsync(item, token);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("Gdpr/Consents/{id}")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse(typeof(Person))]
        public override Task<Consent> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.ReadAsync(id, token);
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("Gdpr/Consents/{id}")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse]
        public override Task UpdateAsync(string id, Consent item, CancellationToken token = new CancellationToken())
        {
            return base.UpdateAsync(id, item, token);
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("Gdpr/Consents/{id}")]
        [SwaggerGroup("Consents")]
        [SwaggerSuccessResponse]
        public override Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.DeleteAsync(id, token);
        }
    }
}

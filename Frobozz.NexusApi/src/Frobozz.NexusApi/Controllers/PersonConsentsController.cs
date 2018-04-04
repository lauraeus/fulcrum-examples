using System.Web.Http;
using Frobozz.CapabilityContracts.ApiHelpers;
using Frobozz.CapabilityContracts.Gdpr;

namespace Frobozz.NexusApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons/{parentId}/Consents")]
    public class PersonConsentsController : ApiManyToOneController<PersonConsent>, IPersonConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentsController(IGdprCapability gdprCapability)
            : base(gdprCapability.PersonConsentService)
        {
        }
    }
}

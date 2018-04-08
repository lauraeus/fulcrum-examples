using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.MoveTo.WebApi.ApiControllerHelpers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons/{parentId}/Consents")]
    public class PersonConsentsController : ManyToOneApiController<PersonConsent>
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentsController(IGdprCapability logic)
        :base(logic.PersonConsentService)
        {
        }
    }
}

using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel;
using Xlent.Lever.Libraries2.MoveTo.WebApi.ApiControllerHelpers;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Controllers
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
        public PersonConsentsController(IGdprCapability<PersonX> logic)
        :base(logic.PersonConsentService)
        {
        }
    }
}

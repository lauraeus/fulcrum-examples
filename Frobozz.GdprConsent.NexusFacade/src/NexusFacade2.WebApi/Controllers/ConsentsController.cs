using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.MoveTo.WebApi.ApiControllerHelpers;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Consents")]
    public class ConsentsController : ApiCrudController<Consent>, IConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentsController(IGdprCapability logic)
        :base(logic.ConsentService)
        {
        }
    }
}

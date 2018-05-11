using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.Contracts.WebApi.GdprCapability.Controllers;
using Xlent.Lever.Libraries2.WebApi.Crud.ApiControllers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Consents")]
    public class ConsentsController : ConsentServiceController
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentsController(IGdprCapability logic)
        :base(logic)
        {
        }
    }
}

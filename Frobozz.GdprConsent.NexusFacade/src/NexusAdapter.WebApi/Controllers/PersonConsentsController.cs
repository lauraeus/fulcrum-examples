using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.AspNet.GdprCapability.Controllers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api")]
    public class PersonConsentsController : PersonConsentServiceController
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentsController(IGdprCapability logic)
        :base(logic)
        {
        }
    }
}

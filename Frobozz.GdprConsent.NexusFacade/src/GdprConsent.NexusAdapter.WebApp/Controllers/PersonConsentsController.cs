using Frobozz.Contracts.AspNet.GdprCapability.Controllers;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [Route("api")]
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

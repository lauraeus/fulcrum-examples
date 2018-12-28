using Frobozz.Contracts.AspNet.GdprCapability.Controllers;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Frobozz.GdprConsent.NexusAdapter.WebApp.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [Route("api")]
    public class PersonsController : PersonServiceController
    {
        private readonly IGdprCapability _logic;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonsController(IGdprCapability logic)
        :base(logic)
        {
            _logic = logic;
        }
    }
}

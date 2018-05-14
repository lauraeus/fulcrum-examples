using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.WebApi.GdprCapability.Controllers;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api")]
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

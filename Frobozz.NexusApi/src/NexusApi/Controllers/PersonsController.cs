using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.WebApi.GdprCapability.Controllers;

namespace Frobozz.NexusApi.Controllers
{
    /// <inheritdoc />
    [RoutePrefix("api")]
    public class PersonsController : PersonServiceController
    {
        /// <inheritdoc />
        public PersonsController(IGdprCapability gdprCapability)
        :base(gdprCapability)
        {
        }
    }
}

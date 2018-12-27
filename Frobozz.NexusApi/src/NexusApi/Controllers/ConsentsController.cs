using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.AspNet.GdprCapability.Controllers;

namespace Frobozz.NexusApi.Controllers
{
    /// <inheritdoc />
    [RoutePrefix("api")]
    public class ConsentsController : ConsentServiceController
    {
        /// <inheritdoc />
        public ConsentsController(IGdprCapability gdprCapability)
            : base(gdprCapability)
        {
        }
    }
}

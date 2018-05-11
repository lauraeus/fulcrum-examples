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
    [RoutePrefix("api/Persons/{parentId}/Consents")]
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

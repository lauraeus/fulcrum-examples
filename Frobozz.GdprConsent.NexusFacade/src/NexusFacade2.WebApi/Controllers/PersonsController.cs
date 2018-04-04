using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.ServiceModel;
using Xlent.Lever.Libraries2.MoveTo.WebApi.ApiControllerHelpers;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons")]
    public class PersonsController : ApiCrudController<PersonX>, IPersonService<PersonX>
    {
        private readonly IGdprCapability<PersonX> _logic;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonsController(IGdprCapability<PersonX> logic)
        :base(logic.PersonService)
        {
            _logic = logic;
        }

        /// <inheritdoc />
        public async Task<PersonX> GetRandomAsync()
        {
            return await _logic.PersonService.GetRandomAsync();
        }
    }
}

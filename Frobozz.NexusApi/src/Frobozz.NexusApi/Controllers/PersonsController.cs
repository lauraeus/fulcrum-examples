using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.ApiHelpers;
using Frobozz.CapabilityContracts.Gdpr;

namespace Frobozz.NexusApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons")]
    public class PersonsController : ApiCrudController<Person>, IPersonService
    {
        private readonly IGdprCapability _gdprCapability;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonsController(IGdprCapability gdprCapability)
        :base(gdprCapability.PersonService)
        {
            _gdprCapability = gdprCapability;
        }

        /// <inheritdoc />
        public async Task<Person> GetRandomAsync()
        {
            return await _gdprCapability.PersonService.GetRandomAsync();
        }
    }
}

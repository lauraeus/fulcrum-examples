using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Error.Logic;
using Xlent.Lever.Libraries2.WebApi.Crud.ApiControllers;

namespace Frobozz.NexusApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/Persons")]
    public class PersonsController : CrudApiController<Person>, IPersonService
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
        [HttpGet]
        [Route("FindByName")]
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhitespace(name, nameof(name));
            var result = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            return result;
        }

        /// Intentionally disabled.
        private new Task DeleteAllAsync(CancellationToken token = default(CancellationToken)) => throw new FulcrumNotImplementedException(nameof(DeleteAllAsync));
    }
}

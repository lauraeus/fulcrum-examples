using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.WebApi.Annotations;
using Xlent.Lever.Libraries2.WebApi.Crud.ApiControllers;

namespace Frobozz.Contracts.WebApi.GdprCapability.Controllers
{
    /// <inheritdoc cref="IPersonService" />
    public abstract class PersonServiceController : CrudApiController<PersonCreate, Person>, IPersonService
    {
        private readonly IGdprCapability _gdprCapability;

        /// <summary>
        /// Constructor
        /// </summary>
        protected PersonServiceController(IGdprCapability gdprCapability)
        :base(gdprCapability.PersonService)
        {
            _gdprCapability = gdprCapability;
        }

        /// <inheritdoc />
        /// <summary>
        /// Get the person with the specified <paramref name="id" />.
        /// </summary>
        /// <param name="id">The id of the person to get.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
        [HttpGet]
        [Route("Gdpr/Persons/{id}")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(Person))]
        public override Task<Person> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.ReadAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Find the first person with an exact match for the name
        /// </summary>
        /// <param name="name">The name to have an exact match for.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
        [HttpGet]
        [Route("Gdpr/Persons/FindByName")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(Person))]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhitespace(name, nameof(name));
            var result = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            return result;
        }
    }
}

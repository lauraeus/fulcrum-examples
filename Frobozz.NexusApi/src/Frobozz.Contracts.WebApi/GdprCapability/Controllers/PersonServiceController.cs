using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.Crud.Interfaces;
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
        /// Create a new person
        /// </summary>
        /// <param name="person">The data for the person to create.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
        [HttpPost]
        [Route("Gdpr/Persons")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(string))]
        public override Task<string> CreateAsync(PersonCreate person, CancellationToken token = new CancellationToken())
        {
            return base.CreateAsync(person, token);
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
        /// Get all persons
        /// </summary>
        /// <param name="offset">The number of items that will be skipped in result.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
        [HttpGet]
        [Route("Gdpr/Persons")]
        [SwaggerGroup("Persons")]
        [SwaggerSuccessResponse(typeof(PageEnvelope<Person>))]
        public override Task<PageEnvelope<Person>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = new CancellationToken())
        {
            return base.ReadAllWithPagingAsync(offset, limit, token);
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

using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.AspNet.Controllers;
using Nexus.Link.Libraries.Web.AspNet.Annotations;

namespace Frobozz.Contracts.AspNet.GdprCapability.Controllers
{
    /// <inheritdoc cref="IPersonService" />
    public abstract class PersonServiceController : CrudController<PersonCreate, Person>, IPersonService
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
        /// Create a person
        /// </summary>
        /// <param name="item">The data for the person to create.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
        [HttpPost]
        [Route("Gdpr/Persons")]
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerSuccessResponse(typeof(string))]
        public override Task<string> CreateAsync(PersonCreate item, CancellationToken token = new CancellationToken())
        {
            return base.CreateAsync(item, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a specific person.
        /// </summary>
        /// <param name="id">The id of the person to get.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
        [HttpGet]
        [Route("Gdpr/Persons/{id}")]
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerSuccessResponse(typeof(Person))]
        public override Task<Person> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.ReadAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a page of persons.
        /// </summary>
        /// <param name="offset">The number of items that will be skipped in result.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
        [HttpGet]
        [Route("Gdpr/Persons")]
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerSuccessResponse(typeof(PageEnvelope<Person>))]
        public override Task<PageEnvelope<Person>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = new CancellationToken())
        {
            return base.ReadAllWithPagingAsync(offset, limit, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Update the information for a person.
        /// </summary>
        /// <param name="id">The id of the person to update.</param>
        /// <param name="item">The updated information for the person.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
        [HttpPut]
        [Route("Gdpr/Persons/{id}")]
        [SwaggerGroup("Gdpr/Persons")]
        public override Task UpdateAsync(string id, Person item, CancellationToken token = new CancellationToken())
        {
            return base.UpdateAsync(id, item, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id">The id of the person to delete.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
        [HttpDelete]
        [Route("Gdpr/Persons/{id}")]
        [SwaggerGroup("Gdpr/Persons")]
        public override Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            return base.DeleteAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete all persons
        /// </summary>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
        [HttpDelete]
        [Route("Gdpr/Persons")]
        [SwaggerGroup("Gdpr/Persons")]
        public override Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            return base.DeleteAllAsync(token);
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
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerSuccessResponse(typeof(Person))]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhiteSpace(name, nameof(name));
            var result = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            return result;
        }
    }
}

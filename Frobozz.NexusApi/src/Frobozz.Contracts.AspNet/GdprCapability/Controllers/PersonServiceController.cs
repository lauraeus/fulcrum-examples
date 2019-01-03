using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Core.Storage.Model;
using Nexus.Link.Libraries.Crud.AspNet.ControllerHelpers;
using Nexus.Link.Libraries.Crud.AspNet.Controllers;
using Nexus.Link.Libraries.Web.AspNet.Annotations;
#if NETCOREAPP
using Microsoft.AspNetCore.Mvc;
#elif NET472
using System.Web.Http;
#endif

namespace Frobozz.Contracts.AspNet.GdprCapability.Controllers
{
    /// <inheritdoc cref="IPersonService" />
    public abstract class PersonServiceController :
#if NETCOREAPP
        ControllerBase
#elif NET472
        ApiController
# endif
        //: IPersonService
    {
        private readonly IGdprCapability _gdprCapability;
        private CrudControllerHelper<PersonCreate, Person> _helper;

        /// <summary>
        /// Constructor
        /// </summary>
        protected PersonServiceController(IGdprCapability gdprCapability)
        {
            _helper = new CrudControllerHelper<PersonCreate, Person>(gdprCapability.PersonService);
                _gdprCapability = gdprCapability;
        }
        
        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="item">The data for the person to create.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
#if NETCOREAPP
        [HttpPost("Gdpr/Persons")]
#elif NET472
        [HttpPost]
        [Route("Gdpr/Persons")]
#endif
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerSuccessResponse(typeof(string))]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public Task<string> CreateAsync(PersonCreate item, CancellationToken token = new CancellationToken())
        {
            return _helper.CreateAsync(item, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a specific person.
        /// </summary>
        /// <param name="id">The id of the person to get.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
#if NETCOREAPP
        [HttpGet("Gdpr/Persons/{id}")]
#elif NET472
        [HttpGet]
        [Route("Gdpr/Persons/{id}")]
#endif
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerSuccessResponse(typeof(Person))]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public Task<Person> ReadAsync(string id, CancellationToken token = new CancellationToken())
        {
            return _helper.ReadAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Get a page of persons.
        /// </summary>
        /// <param name="offset">The number of items that will be skipped in result.</param>
        /// <param name="limit">The maximum number of items to return.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
#if NETCOREAPP
        [HttpGet("Gdpr/Persons")]
#elif NET472
        [HttpGet]
        [Route("Gdpr/Persons")]
        [SwaggerGroup("Gdpr/Persons")]
#endif
        [SwaggerSuccessResponse(typeof(PageEnvelope<Person>))]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public Task<PageEnvelope<Person>> ReadAllWithPagingAsync(int offset, int? limit =
 null, CancellationToken token = new CancellationToken())
        {
            return _helper.ReadAllWithPagingAsync(offset, limit, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Update the information for a person.
        /// </summary>
        /// <param name="id">The id of the person to update.</param>
        /// <param name="item">The updated information for the person.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
#if NETCOREAPP
        [HttpPut("Gdpr/Persons/{id}")]
        [ApiExplorerSettings(GroupName = "Gdpr/Persons")]
#elif NET472
        [HttpPut]
        [Route("Gdpr/Persons/{id}")]
        [SwaggerGroup("Gdpr/Persons")]
#endif
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public Task UpdateAsync(string id, Person item, CancellationToken token = new CancellationToken())
        {
            return _helper.UpdateAsync(id, item, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id">The id of the person to delete.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
#if NETCOREAPP
        [HttpDelete("Gdpr/Persons/{id}")]
#elif NET472
        [HttpDelete]
        [Route("Gdpr/Persons/{id}")]
#endif
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public Task DeleteAsync(string id, CancellationToken token = new CancellationToken())
        {
            return _helper.DeleteAsync(id, token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Delete all persons
        /// </summary>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The id for the created person.</returns>
#if NETCOREAPP
        [HttpDelete("Gdpr/Persons")]
#elif NET472
        [HttpDelete]
        [Route("Gdpr/Persons")]
#endif
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public Task DeleteAllAsync(CancellationToken token = new CancellationToken())
        {
            return _helper.DeleteAllAsync(token);
        }

        /// <inheritdoc />
        /// <summary>
        /// Find the first person with an exact match for the name
        /// </summary>
        /// <param name="name">The name to have an exact match for.</param>
        /// <param name="token">Propagates notification that operations should be canceled.</param>
        /// <returns>The found person or null.</returns>
#if NETCOREAPP
        [HttpGet("Gdpr/Persons/FindByName")]
#elif NET472
        [HttpGet]
        [Route("Gdpr/Persons/FindByName")]
#endif
        [SwaggerGroup("Gdpr/Persons")]
        [SwaggerSuccessResponse(typeof(Person))]
        [SwaggerBadRequestResponse]
        [SwaggerInternalServerErrorResponse]
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token =
 default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhiteSpace(name, nameof(name));
            var result = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            return result;
        }
    }
}

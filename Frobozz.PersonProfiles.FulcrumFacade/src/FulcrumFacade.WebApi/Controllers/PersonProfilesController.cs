using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.PersonProfiles.Bll;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/v1/PersonProfiles")]
    public class PersonProfilesController : ApiController, IPersonProfilesService
    {
        private static readonly string Namespace = typeof(PersonProfilesController).Namespace;
        /// <summary>
        /// The actual implementation
        /// </summary>
        public IPersonProfilesFunctionality PersonProfilesFunctionality { get; }

        /// <summary>
        /// Constructor that takes a logic layer for product. 
        /// </summary>
        /// <param name="personProfilesFunctionality">Dependency injected logic layer</param>
        public PersonProfilesController(IPersonProfilesFunctionality personProfilesFunctionality)
        {
            PersonProfilesFunctionality = personProfilesFunctionality;
        }

        /// <inheritdoc />
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("")]
        public async Task<PersonProfile> CreateAsync(PersonProfile item)
        {
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            var personProfile = await PersonProfilesFunctionality.CreateAsync(item);
            FulcrumAssert.IsNotNull(personProfile, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            FulcrumAssert.IsValidated(personProfile, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            return personProfile;
        }

        /// <inheritdoc />
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("")]
        public async Task<PersonProfile> ReadAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            var personProfile = await PersonProfilesFunctionality.ReadAsync(id);
            FulcrumAssert.IsNotNull(personProfile, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            FulcrumAssert.IsValidated(personProfile, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            return personProfile;
        }

        /// <inheritdoc />
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("")]
        public async Task<PersonProfile> UpdateAsync(PersonProfile item)
        {
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            var personProfile = await PersonProfilesFunctionality.UpdateAsync(item);
            FulcrumAssert.IsNotNull(personProfile, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            FulcrumAssert.IsValidated(personProfile, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            return personProfile;
        }

        /// <inheritdoc />
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("")]
        public async Task DeleteAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            await PersonProfilesFunctionality.DeleteAsync(id);
        }
    }
}

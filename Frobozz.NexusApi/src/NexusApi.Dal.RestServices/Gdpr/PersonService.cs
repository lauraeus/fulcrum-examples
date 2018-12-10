using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Rest;
using Nexus.Link.Libraries.Core.Platform.Authentication;
using Nexus.Link.Libraries.Crud.Web.RestClient;
using Nexus.Link.Libraries.Web.RestClientHelper;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class PersonService : CrudRestClient<PersonCreate, Person, string>, IPersonService
    {
        /// <inheritdoc />
        public PersonService(string baseUri, bool withLogging = true) : base(baseUri, withLogging)
        {
        }

        /// <inheritdoc />
        public PersonService(string baseUri, ServiceClientCredentials credentials, bool withLogging = true) : base(baseUri, credentials, withLogging)
        {
        }

        /// <inheritdoc />
        public PersonService(string baseUri, AuthenticationToken authenticationToken, bool withLogging) : base(baseUri, authenticationToken, withLogging)
        {
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var safeName = HttpUtility.UrlEncode(name);
            return await GetAsync<Person>($"FindByName?name={safeName}", cancellationToken: token);
        }
    }
}
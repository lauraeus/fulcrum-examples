using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Rest;
using Nexus.Link.Libraries.Crud.Web.RestClient;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class PersonService : CrudRestClient<PersonCreate, Person, string>, IPersonService
    {
        /// <inheritdoc />
        public PersonService(string baseUri, HttpClient client) : base(baseUri, client)
        {
        }

        /// <inheritdoc />
        public PersonService(string baseUri, ServiceClientCredentials credentials, HttpClient client) : base(baseUri, client, credentials)
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
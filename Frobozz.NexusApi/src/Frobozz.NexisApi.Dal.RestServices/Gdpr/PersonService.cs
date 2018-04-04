using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Microsoft.Rest;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class PersonService : RestClientCrud<Person, string>, IPersonService
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
        public async Task<Person> GetRandomAsync()
        {
            return await GetAsync<Person>("GetRandom");
        }
    }
}
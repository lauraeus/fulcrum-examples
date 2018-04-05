using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var safeName = HttpUtility.UrlEncode(name);
            return await GetAsync<Person>($"?name={safeName}", cancellationToken: token);
        }
    }
}
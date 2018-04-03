using Frobozz.CapabilityContracts.Gdpr;
using Microsoft.Rest;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.NexusApi.RestServices.GdprCapability
{
    internal class PersonService : RestClientCrud<Person, string>
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
    }
}
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Microsoft.Rest;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.NexusApi.RestServices.GdprCapability
{
    internal class ConsentService : RestClientCrud<Consent, string>, IConsentService
    {
        /// <inheritdoc />
        public ConsentService(string baseUri, bool withLogging = true) : base(baseUri, withLogging)
        {
        }

        /// <inheritdoc />
        public ConsentService(string baseUri, ServiceClientCredentials credentials, bool withLogging = true) : base(baseUri, credentials, withLogging)
        {
        }

        /// <inheritdoc />
        public ConsentService(string baseUri, AuthenticationToken authenticationToken, bool withLogging) : base(baseUri, authenticationToken, withLogging)
        {
        }
    }
}
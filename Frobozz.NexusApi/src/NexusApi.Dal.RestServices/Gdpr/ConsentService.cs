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
    internal class ConsentService : CrudRestClient<ConsentCreate, Consent, string>, IConsentService
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
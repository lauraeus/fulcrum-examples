using System.Net.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Web.Pipe.Outbound;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability
    {
        private static HttpClient _httpClient;
        /// <inheritdoc />
        public GdprCapability()
        {
            var delegatingHandlers = OutboundPipeFactory.CreateDelegatingHandlers();
            _httpClient = HttpClientFactory.Create(delegatingHandlers);
            PersonService = new PersonService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Persons", _httpClient);
            ConsentService = new ConsentService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Consents", _httpClient);
            FulcrumAssert.IsNotNull(ConsentService);
            PersonConsentService = new PersonConsentService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Persons", _httpClient, "Persons", "Consents");
            ConsentPersonService = new ConsentPersonService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Consents", _httpClient, "Consents", "Persons");
            FulcrumAssert.IsNotNull(PersonConsentService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }

        /// <inheritdoc />
        public IConsentPersonService ConsentPersonService { get; }
    }
}
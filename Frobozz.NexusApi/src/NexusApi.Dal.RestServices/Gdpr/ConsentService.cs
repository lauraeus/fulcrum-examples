using System.Net.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Rest;
using Nexus.Link.Libraries.Crud.Web.RestClient;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class ConsentService : CrudRestClient<ConsentCreate, Consent, string>, IConsentService
    {
        /// <inheritdoc />
        public ConsentService(string baseUri, HttpClient client) : base(baseUri, client)
        {
        }

        /// <inheritdoc />
        public ConsentService(string baseUri, ServiceClientCredentials credentials, HttpClient client) : base(baseUri, client, credentials)
        {
        }
    }
}
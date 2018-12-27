using System.Net.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Rest;
using Nexus.Link.Libraries.Core.Platform.Authentication;
using Nexus.Link.Libraries.Crud.Web.RestClient;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class PersonConsentService : CrudSlaveToMasterRestClient<PersonConsentCreate, PersonConsent, string>, IPersonConsentService
    {
        /// <inheritdoc />
        public PersonConsentService(string baseUri, HttpClient client, string parentName = "Parent", string childrenName = "Children") 
            : base(baseUri, client, parentName, childrenName)
        {
        }

        /// <inheritdoc />
        public PersonConsentService(string baseUri, HttpClient client, ServiceClientCredentials credentials, string parentName = "Parent", string childrenName = "Children")
            : base(baseUri, client, credentials, parentName, childrenName)
        {
        }
    }
}
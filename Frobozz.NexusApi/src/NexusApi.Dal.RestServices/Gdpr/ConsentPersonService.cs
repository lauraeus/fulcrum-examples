using System.Net.Http;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Rest;
using Nexus.Link.Libraries.Core.Platform.Authentication;
using Nexus.Link.Libraries.Crud.Web.RestClient;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class ConsentPersonService : CrudSlaveToMasterRestClient<PersonConsentCreate, PersonConsent, string>, IConsentPersonService
    {
        /// <inheritdoc />
        public ConsentPersonService(string baseUri, HttpClient client, string parentName = "Parent", string childrenName = "Children") 
            : base(baseUri, client, parentName, childrenName)
        {
        }

        /// <inheritdoc />
        public ConsentPersonService(string baseUri, HttpClient client, ServiceClientCredentials credentials, string parentName = "Parent", string childrenName = "Children") 
            : base(baseUri, client, credentials, parentName, childrenName)
        {
        }
    }
}
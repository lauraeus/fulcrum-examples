using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Rest;
using Nexus.Link.Libraries.Core.Platform.Authentication;
using Nexus.Link.Libraries.Crud.NetFramework.WebApi.RestClient;
using Nexus.Link.Libraries.Web.RestClientHelper;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class ConsentPersonService : CrudSlaveToMasterRestClient<PersonConsentCreate, PersonConsent, string>, IConsentPersonService
    {
        /// <inheritdoc />
        public ConsentPersonService(string baseUri, string parentName = "Parent", string childrenName = "Children", bool withLogging = true) : base(baseUri, parentName, childrenName, withLogging)
        {
        }

        /// <inheritdoc />
        public ConsentPersonService(string baseUri, ServiceClientCredentials credentials, string parentName = "Parent", string childrenName = "Children", bool withLogging = true) : base(baseUri, credentials, parentName, childrenName, withLogging)
        {
        }

        /// <inheritdoc />
        public ConsentPersonService(string baseUri, AuthenticationToken authenticationToken, string parentName = "Parent", string childrenName = "Children", bool withLogging = true) : base(baseUri, authenticationToken, parentName, childrenName, withLogging)
        {
        }
    }
}
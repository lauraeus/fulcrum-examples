using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Rest;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;
using Xlent.Lever.Libraries2.WebApi.Crud.RestClient;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    internal class ConsentPersonService : SlaveToMasterRestClient<PersonConsentCreate, PersonConsent, string>, IConsentPersonService
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
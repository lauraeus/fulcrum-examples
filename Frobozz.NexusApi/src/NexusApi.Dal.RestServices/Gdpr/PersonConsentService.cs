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
    internal class PersonConsentService : SlaveToMasterRestClient<PersonConsentCreate, PersonConsent, string>, IPersonConsentService
    {
        /// <inheritdoc />
        public PersonConsentService(string baseUri, string parentName = "Parent", string childrenName = "Children", bool withLogging = true) : base(baseUri, parentName, childrenName, withLogging)
        {
        }

        /// <inheritdoc />
        public PersonConsentService(string baseUri, ServiceClientCredentials credentials, string parentName = "Parent", string childrenName = "Children", bool withLogging = true) : base(baseUri, credentials, parentName, childrenName, withLogging)
        {
        }

        /// <inheritdoc />
        public PersonConsentService(string baseUri, AuthenticationToken authenticationToken, string parentName = "Parent", string childrenName = "Children", bool withLogging = true) : base(baseUri, authenticationToken, parentName, childrenName, withLogging)
        {
        }
    }
}
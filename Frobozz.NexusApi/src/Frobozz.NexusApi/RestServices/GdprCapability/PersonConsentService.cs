using Frobozz.CapabilityContracts.Gdpr;
using Microsoft.Rest;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.NexusApi.RestServices.GdprCapability
{
    internal class PersonConsentService : RestClientManyToOne<Consent, string>
    {
        private new const string ParentName = "Persons";
        private new const string ChildrenName = "Consents";
        /// <inheritdoc />
        public PersonConsentService(string baseUri, bool withLogging = true) : base(baseUri, ParentName, ChildrenName, withLogging)
        {
        }

        /// <inheritdoc />
        public PersonConsentService(string baseUri, ServiceClientCredentials credentials, bool withLogging = true) : base(baseUri, credentials, ParentName, ChildrenName, withLogging)
        {
        }

        /// <inheritdoc />
        public PersonConsentService(string baseUri, AuthenticationToken authenticationToken, bool withLogging = true) : base(baseUri, authenticationToken, ParentName, ChildrenName, withLogging)
        {
        }
    }
}
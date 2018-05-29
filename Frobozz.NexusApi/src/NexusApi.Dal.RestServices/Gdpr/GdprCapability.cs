using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.WebApi.Crud.RestClient;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.NexusApi.Dal.RestServices.Gdpr
{
    /// <inheritdoc />
    public class GdprCapability : IGdprCapability
    {
        /// <inheritdoc />
        public GdprCapability()
        {
            PersonService = new PersonService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Persons");
            ConsentService = new ConsentService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Consents");
            FulcrumAssert.IsNotNull(ConsentService);
            PersonConsentService = new PersonConsentService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Persons", "Persons", "Consents");
            ConsentPersonService = new ConsentPersonService("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Consents", "Consents", "Persons");
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
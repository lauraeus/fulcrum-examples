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
            ConsentService = new CrudRestClient<ConsentCreate, Consent, string>("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Consents");
            FulcrumAssert.IsNotNull(ConsentService);
            PersonConsentService = new SlaveToMasterRestClient<PersonConsentCreate, PersonConsent, string>("http://localhost/GdprConsent.NexusAdapter.WebApi/api/Gdpr/Persons", "Persons", "Consents");
            FulcrumAssert.IsNotNull(PersonConsentService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public ICrudSlaveToMaster<PersonConsentCreate, PersonConsent, string> PersonConsentService { get; }
    }
}
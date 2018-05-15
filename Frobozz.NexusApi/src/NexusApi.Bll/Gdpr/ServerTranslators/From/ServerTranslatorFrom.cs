using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.ServerTranslators.From;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <inheritdoc />
    public class ServerTranslatorFrom : IGdprCapability
    {
        /// <inheritdoc />
        public ServerTranslatorFrom(IGdprCapability capablity, System.Func<string> getServerNameMethod)
        {
            PersonService = new PersonServerTranslatorFrom(capablity, getServerNameMethod);
            ConsentService = new CrudFromServerTranslator<ConsentCreate, Consent>(capablity.ConsentService, "consent.id", getServerNameMethod);
            FulcrumAssert.IsNotNull(ConsentService);
            PersonConsentService = new SlaveToMasterFromServerTranslator<PersonConsentCreate, PersonConsent>(capablity.PersonConsentService, "person.id" ,"person-consent.id", getServerNameMethod);
            FulcrumAssert.IsNotNull(PersonConsentService);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public ISlaveToMaster<PersonConsentCreate, PersonConsent, string> PersonConsentService { get; }
    }
}
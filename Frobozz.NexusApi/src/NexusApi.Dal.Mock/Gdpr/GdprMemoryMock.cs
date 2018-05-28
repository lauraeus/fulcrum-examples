using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.Interfaces;
using Xlent.Lever.Libraries2.Crud.MemoryStorage;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc />
    public class GdprMemoryMock : IGdprCapability
    {
        /// <inheritdoc />
        public GdprMemoryMock()
        {
            ConsentService = new CrudMemory<ConsentCreate, Consent, string>();
            FulcrumAssert.IsNotNull(ConsentService);
            PersonService = new PersonMemoryStorage();
            PersonConsentService = new SlaveToMasterMemory<PersonConsentCreate, PersonConsent, string>();
            FulcrumAssert.IsNotNull(PersonConsentService);
        }

        /// <inheritdoc />
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrudSlaveToMaster<PersonConsentCreate, PersonConsent, string> PersonConsentService { get; }
    }
}
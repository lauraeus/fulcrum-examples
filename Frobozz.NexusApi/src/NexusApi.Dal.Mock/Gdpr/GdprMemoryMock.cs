using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.MemoryStorage;

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
            PersonConsentService = new ManyToOneMemory<PersonConsent, string>(consent => consent.PersonId);
            FulcrumAssert.IsNotNull(PersonConsentService);
        }

        /// <inheritdoc />
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IManyToOne<PersonConsent, string> PersonConsentService { get; }
    }
}
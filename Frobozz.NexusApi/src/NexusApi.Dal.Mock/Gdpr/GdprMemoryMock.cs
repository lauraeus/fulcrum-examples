using Frobozz.Contracts.GdprCapability.Interfaces;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc />
    public class GdprMemoryMock : IGdprCapability
    {
        /// <inheritdoc />
        public GdprMemoryMock()
        {
            PersonService = new PersonMemoryStorage();
            ConsentService = new ConsentMemoryStorage();
            PersonConsentService = new PersonConsentMemoryStorage();
            ConsentPersonService = new ConsentPersonMemoryStorage();
        }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }

        /// <inheritdoc />
        public IConsentPersonService ConsentPersonService { get; }
    }
}
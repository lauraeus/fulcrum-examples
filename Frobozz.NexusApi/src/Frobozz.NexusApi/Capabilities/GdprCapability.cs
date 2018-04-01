using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.NexusApi.Capabilities
{
    public class GdprCapability : IGdprCapability
    {
        public GdprCapability(ICrud<Person, string> person, ICrud<Consent, string> consent, IManyToOneRelation<Consent, string> personConsent)
        {
            Person = person;
            Consent = consent;
            PersonConsent = personConsent;
        }

        /// <inheritdoc />
        public ICrud<Person, string> Person { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> Consent { get; }

        /// <inheritdoc />
        public IManyToOneRelation<Consent, string> PersonConsent { get; }
    }
}
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Gdpr.Logic
{
    public interface IGdprCapability 
    {
        IPersonService PersonService { get; }

        ICrud<Consent, string> ConsentService { get; }

        IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}

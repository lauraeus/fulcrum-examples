using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;

namespace Frobozz.CapabilityContracts.Gdpr.Logic
{
    public interface IGdprCapability 
    {
        IPersonService PersonService { get; }

        ICrud<Consent, string> ConsentService { get; }

        IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}

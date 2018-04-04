namespace Frobozz.CapabilityContracts.Gdpr
{
    public interface IGdprCapability<TPerson> 
        where TPerson : Person
    {
        IPersonService<TPerson> PersonService { get; }

        IConsentService ConsentService { get; }

        IPersonConsentService PersonConsentService { get; }
    }
}

namespace Frobozz.CapabilityContracts.Gdpr
{
    public interface IGdprCapability 
    {
        IPersonService PersonService { get; }

        IConsentService ConsentService { get; }

        IPersonConsentService PersonConsentService { get; }
    }
}

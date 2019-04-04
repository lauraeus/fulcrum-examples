namespace TheSystem.NexusAdapter.Service.CrmSystemContract
{
    public interface ICrmSystem
    {
        ILeadFunctionality LeadFunctionality { get; }
        IContactFunctionality ContactFunctionality { get; }
    }
}

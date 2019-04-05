namespace TheSystem.NexusAdapter.Service.System.CrmSystemContract
{
    public interface ICrmSystem
    {
        ILeadFunctionality LeadFunctionality { get; }
        IContactFunctionality ContactFunctionality { get; }
    }
}

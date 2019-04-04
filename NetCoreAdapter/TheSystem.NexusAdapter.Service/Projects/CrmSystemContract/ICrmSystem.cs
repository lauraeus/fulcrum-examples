namespace TheSystem.NexusAdapter.Service.Projects.CrmSystemContract
{
    public interface ICrmSystem
    {
        ILeadFunctionality LeadFunctionality { get; }
        IContactFunctionality ContactFunctionality { get; }
    }
}

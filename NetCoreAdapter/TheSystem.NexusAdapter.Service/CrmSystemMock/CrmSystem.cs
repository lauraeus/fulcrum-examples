using TheSystem.NexusAdapter.Service.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.CrmSystemMock
{
    public class CrmSystem : ICrmSystem
    {
        /// <inheritdoc />
        public CrmSystem()
        {
            var contactService = new ContactFunctionality();
            ContactFunctionality = contactService;
            LeadFunctionality = new LeadFunctionality(contactService);
        }

        /// <inheritdoc />
        public ILeadFunctionality LeadFunctionality { get; }

        /// <inheritdoc />
        public IContactFunctionality ContactFunctionality { get; }
    }
}

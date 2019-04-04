using TheSystem.NexusAdapter.Service.Projects.AdapterContract;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.Projects.CrmSystemMock
{
    public class CrmSystemMock : ICrmSystem
    {
        /// <inheritdoc />
        public CrmSystemMock(IAdapterService adapterService)
        {
            var contactService = new ContactFunctionality();
            ContactFunctionality = contactService;
            LeadFunctionality = new LeadFunctionality(contactService, adapterService);
        }

        /// <inheritdoc />
        public ILeadFunctionality LeadFunctionality { get; }

        /// <inheritdoc />
        public IContactFunctionality ContactFunctionality { get; }
    }
}

using TheSystem.NexusAdapter.Service.Adapter.Contract;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.Mock.CrmSystemMock
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

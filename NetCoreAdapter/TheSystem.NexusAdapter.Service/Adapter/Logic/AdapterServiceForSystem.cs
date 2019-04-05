using System;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.Adapter.Contract;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.Events;
using TheSystem.NexusAdapter.Service.NexusApi.NexusApiContract;

namespace TheSystem.NexusAdapter.Service.Adapter.Logic
{
    public class AdapterServiceForSystem : IAdapterService
    {
        private readonly INexusApi _nexusApi;

        public AdapterServiceForSystem(INexusApi nexusApi)
        {
            _nexusApi = nexusApi;
        }

        /// <inheritdoc />
        public async Task LeadWasQualified(Guid leadId, Guid contactId, DateTimeOffset approvedAt)
        {
            var @event = new MemberApprovedEvent
            {
                MemberId = contactId.ToIdString(),
                ApprovedAt = approvedAt.ToIso8061Time()
            };
            await _nexusApi.BusinessEventService.PublishAsync(@event);
        }
    }
}

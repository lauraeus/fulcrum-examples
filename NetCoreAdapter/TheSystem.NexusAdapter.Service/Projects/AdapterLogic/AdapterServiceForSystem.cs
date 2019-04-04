using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.Projects.AdapterContract;
using TheSystem.NexusAdapter.Service.Projects.CapabilityContracts.Events;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemMock;
using TheSystem.NexusAdapter.Service.Projects.NexusApiContract;

namespace TheSystem.NexusAdapter.Service.Projects.AdapterLogic
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

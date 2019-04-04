using System;
using System.Threading.Tasks;

namespace TheSystem.NexusAdapter.Service.Projects.AdapterContract
{
    public interface IAdapterService
    {
        Task LeadWasQualified(Guid leadId, Guid contactId, DateTimeOffset approvedAt);
    }
}
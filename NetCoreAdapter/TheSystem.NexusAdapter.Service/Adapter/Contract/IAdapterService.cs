using System;
using System.Threading.Tasks;

namespace TheSystem.NexusAdapter.Service.Adapter.Contract
{
    public interface IAdapterService
    {
        Task LeadWasQualified(Guid leadId, Guid contactId, DateTimeOffset approvedAt);
    }
}
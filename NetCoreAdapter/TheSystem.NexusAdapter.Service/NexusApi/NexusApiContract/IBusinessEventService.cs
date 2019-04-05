using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.Events;

namespace TheSystem.NexusAdapter.Service.NexusApi.NexusApiContract
{
    /// <summary>
    /// Services for BusinessEvents
    /// </summary>
    public interface IBusinessEventService
    {
        /// <summary>
        /// Publish an event
        /// </summary>
        /// <param name="event">The event to publish.</param>
        Task PublishAsync(IPublishableEvent @event);
    }
}

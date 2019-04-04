using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.Projects.CapabilityContracts.Events;

namespace TheSystem.NexusAdapter.Service.Projects.NexusApiContract
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSystem.NexusAdapter.Service.Projects.NexusApiContract
{
    /// <summary>
    /// The services that the nexus API provides
    /// </summary>
    public interface INexusApi
    {
        /// <summary>
        /// Service for business events
        /// </summary>
        IBusinessEventService BusinessEventService { get; }
    }
}

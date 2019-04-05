namespace TheSystem.NexusAdapter.Service.NexusApi.NexusApiContract
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

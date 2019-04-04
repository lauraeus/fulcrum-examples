namespace TheSystem.NexusAdapter.Service.Projects.CapabilityContracts.Events
{
    /// <summary>
    /// Metadata that are needed for publishing events.
    /// </summary>
    public interface IPublishableEvent
    {
        EventMetadata Metadata { get; }
    }
}
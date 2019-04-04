using TheSystem.NexusAdapter.Service.Projects.NexusApiContract;

namespace TheSystem.NexusAdapter.Service.Projects.NexusApiMock
{
    /// <inheritdoc />
    public class NexusApiMock : INexusApi
    {
        /// <inheritdoc />
        public NexusApiMock()
        {
            BusinessEventService = new BusinessEventService();
        }

        /// <inheritdoc />
        public IBusinessEventService BusinessEventService { get; }
    }
}
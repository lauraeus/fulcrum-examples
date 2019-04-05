using TheSystem.NexusAdapter.Service.NexusApi.NexusApiContract;

namespace TheSystem.NexusAdapter.Service.Mock.NexusApiMock
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
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.Events;
using TheSystem.NexusAdapter.Service.NexusApi.NexusApiContract;

namespace TheSystem.NexusAdapter.Service.Mock.NexusApiMock
{
    /// <inheritdoc />
    public class BusinessEventService : IBusinessEventService
    {
        /// <inheritdoc />
        public Task PublishAsync(IPublishableEvent @event)
        {
            var eventBody = JsonConvert.SerializeObject(@event, Formatting.Indented);
            Console.WriteLine($"POST {@event.Metadata.EntityName}/{@event.Metadata.EventName}/{@event.Metadata.MajorVersion}/{@event.Metadata.MinorVersion}");
            Console.WriteLine(eventBody);
            return Task.CompletedTask;
        }
    }
}
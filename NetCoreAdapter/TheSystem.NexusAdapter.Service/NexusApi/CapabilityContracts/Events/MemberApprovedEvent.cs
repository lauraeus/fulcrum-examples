using Newtonsoft.Json;

namespace TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.Events
{
    /// <summary>
    /// This event is published whenever an applicant is approved to become a member.
    /// </summary>
    public class MemberApprovedEvent : IPublishableEvent
    {
        /// <inheritdoc />
        [JsonIgnore]
        public EventMetadata Metadata { get; } = new EventMetadata("Member", "Approved", 1, 1);

        /// <summary>
        /// The 
        /// </summary>
        public string MemberId { get; set; }

        /// <summary>
        /// The time when the member was approved
        /// </summary>
        public string ApprovedAt { get; set; }
    }
}

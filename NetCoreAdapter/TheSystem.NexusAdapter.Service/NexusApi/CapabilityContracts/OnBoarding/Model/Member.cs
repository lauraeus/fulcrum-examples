namespace TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding.Model
{
    /// <summary>
    /// Information about a member
    /// </summary>
    public class Member
    {
        /// <summary>
        /// The internal id of the member.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unique number to be used in all communication
        /// </summary>
        public string MembershipNumber { get; set; }

        /// <summary>
        /// The name of the member
        /// </summary>
        public string Name { get; set; }
    }
}

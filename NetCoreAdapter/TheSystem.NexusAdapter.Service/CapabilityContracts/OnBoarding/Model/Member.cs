namespace TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding.Model
{
    /// <summary>
    /// Information about a customer
    /// </summary>
    public class Member
    {
        /// <summary>
        /// The internal id of the customer.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The unique number to be used in all communication
        /// </summary>
        public string MembershipNumber { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }
    }
}

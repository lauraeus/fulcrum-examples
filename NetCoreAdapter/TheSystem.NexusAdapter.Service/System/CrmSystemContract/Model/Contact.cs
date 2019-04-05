using System;

namespace TheSystem.NexusAdapter.Service.System.CrmSystemContract.Model
{
    /// <summary>
    /// Information about a customer
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// The internal id of the customer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The lead that led to this contact.
        /// </summary>
        public Guid? OriginatingLeadId { get; set; }

        /// <summary>
        /// The unique customer number to be used in all communication
        /// </summary>
        public string CustomerNumber { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }
    }
}

using System;

namespace TheSystem.NexusAdapter.Service.System.CrmSystemContract.Model
{
    /// <summary>
    /// Information about a lead
    /// </summary>
    public class Lead
    {
        /// <summary>
        /// The id of the object
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the customer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The possible values for lead status
        /// </summary>
        public enum StatusEnum
        {
            /// <summary>
            /// Waiting for qualification
            /// </summary>
            Active,
            /// <summary>
            /// Success
            /// </summary>
            Qualified,
            /// <summary>
            /// Lost
            /// </summary>
            Rejected
        };

        /// <summary>
        /// The current status for the lead
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// Why the lead was rejected or qualified
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        ///  The latest time that the lead was updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.System.CrmSystemContract
{
    /// <summary>
    /// The contract for the lead service
    /// </summary>
    public interface ILeadFunctionality
    {
        /// <summary>
        /// Get a list of all leads
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Lead>> ReadAllAsync();

        /// <summary>
        /// Create one lead
        /// </summary>
        /// <param name="lead">Data for the lead</param>
        /// <returns>The id for the newly created customer</returns>
        Task<Guid> CreateAsync(Lead lead);

        /// <summary>
        /// Qualify a lead to become a customer
        /// </summary>
        /// <param name="id">The id of the lead to qualify.</param>
        /// <returns>The internal id of the new customer record.</returns>
        Task<Guid> QualifyAsync(Guid id);

        /// <summary>
        /// Deny a lead from becoming a customer
        /// </summary>
        /// <param name="id">The id of the lead to qualify.</param>
        /// <param name="reason">The reason behind the rejection</param>
        /// <returns>The internal id of the new customer record.</returns>
        Task RejectAsync(Guid id, string reason);
    }
}
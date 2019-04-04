using System.Collections.Generic;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.CrmSystemContract
{
    /// <summary>
    /// The contract for the customer service
    /// </summary>
    public interface IContactFunctionality
    {
        /// <summary>
        /// Get a list of all customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contact>> ReadAllAsync();
    }
}
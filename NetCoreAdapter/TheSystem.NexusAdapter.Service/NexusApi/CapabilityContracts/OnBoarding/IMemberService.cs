using System.Collections.Generic;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding.Model;

namespace TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding
{
    /// <summary>
    /// Methods for dealing with a member
    /// </summary>
    public interface IMemberService
    {
        /// <summary>
        /// Get a list of all members
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Member>> ReadAllAsync();
    }
}
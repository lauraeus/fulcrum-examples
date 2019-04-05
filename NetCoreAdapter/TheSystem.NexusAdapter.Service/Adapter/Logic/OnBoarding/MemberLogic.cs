using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding.Model;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.Adapter.Logic.OnBoarding
{
    /// <summary>
    /// Implements logic for of <see cref="IMemberService"/>
    /// </summary>
    public class MemberLogic : IMemberService
    {
        private readonly ICrmSystem _crmSystem;

        /// <summary>
        /// Constructor
        /// </summary>
        public MemberLogic(ICrmSystem crmSystem)
        {
            _crmSystem = crmSystem;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Member>> ReadAllAsync()
        {
            var contacts = await _crmSystem.ContactFunctionality.ReadAllAsync();
            var members = contacts.Select(contact => new Member().From(contact));
            return await Task.FromResult(members);
        }
    }
}

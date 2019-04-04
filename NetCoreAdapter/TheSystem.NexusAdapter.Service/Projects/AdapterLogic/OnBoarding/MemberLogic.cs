using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.Projects.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.Projects.CapabilityContracts.OnBoarding.Model;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.Projects.AdapterLogic.OnBoarding
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

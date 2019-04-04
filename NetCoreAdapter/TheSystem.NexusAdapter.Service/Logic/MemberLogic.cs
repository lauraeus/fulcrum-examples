using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding.Model;
using TheSystem.NexusAdapter.Service.CrmSystemContract;
using TheSystem.NexusAdapter.Service.Logic.ModelMapping;

namespace TheSystem.NexusAdapter.Service.Logic
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

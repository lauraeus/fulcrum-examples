using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding.Model;

namespace TheSystem.NexusAdapter.Service.Adapter.Controllers
{
    /// <summary>
    /// Service implementation of <see cref="IMemberService"/>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase, IMemberService
    {
        private readonly IOnBoardingService _capability;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capability">The logic layer</param>
        public MembersController(IOnBoardingService capability)
        {
            _capability = capability;
        }

        /// <inheritdoc />
        [HttpGet]
        public async Task<IEnumerable<Member>> ReadAllAsync()
        {
            return await _capability.MemberService.ReadAllAsync();
        }
    }
}

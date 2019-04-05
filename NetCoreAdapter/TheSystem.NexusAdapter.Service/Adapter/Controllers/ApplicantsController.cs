using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nexus.Link.Libraries.Core.Assert;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding.Model;

namespace TheSystem.NexusAdapter.Service.Adapter.Controllers
{
    /// <summary>
    /// Service implementation of <see cref="IApplicantService"/>
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase, IApplicantService
    {
        private readonly IOnBoardingService _capability;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="capability">The logic layer</param>
        public ApplicantsController(IOnBoardingService capability)
        {
            _capability = capability;
        }

        /// <inheritdoc />
        [HttpGet]
        public async Task<IEnumerable<Applicant>> ReadAllAsync()
        {
            return await _capability.ApplicantService.ReadAllAsync();
        }

        /// <inheritdoc />
        [HttpPost]
        public async Task<string> CreateAsync(Applicant applicant)
        {
            ServiceContract.Require(applicant.Id == null, $"The {nameof(applicant.Id)} field must be null.");
            return await _capability.ApplicantService.CreateAsync(applicant);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Approve")]
        public async Task<string> ApproveAsync(string id)
        {
            return await _capability.ApplicantService.ApproveAsync(id);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Reject")]
        public async Task RejectAsync(string id)
        {
            await _capability.ApplicantService.RejectAsync(id);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/Withdraw")]
        public async Task WithdrawAsync(string id)
        {
            await _capability.ApplicantService.WithdrawAsync(id);
        }
    }
}

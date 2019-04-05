using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding.Model;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.Adapter.Logic.OnBoarding
{
    /// <summary>
    /// Implements logic for of <see cref="IApplicantService"/>
    /// </summary>
    public class ApplicantLogic : IApplicantService
    {
        private readonly ICrmSystem _crmSystem;

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicantLogic(ICrmSystem crmSystem)
        {
            _crmSystem = crmSystem;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Applicant>> ReadAllAsync()
        {
            var leads = await _crmSystem.LeadFunctionality.ReadAllAsync();
            var applicants = leads
                .Where(lead => lead.Status == Lead.StatusEnum.Active)
                .Select(lead => new Applicant().From(lead));
            return await Task.FromResult(applicants);
        }

        /// <inheritdoc />
        public async Task<string> CreateAsync(Applicant applicant)
        {
            var lead = new Lead().From(applicant);
            var id = await _crmSystem.LeadFunctionality.CreateAsync(lead);
            return id.ToIdString();
        }

        /// <inheritdoc />
        public async Task<string> ApproveAsync(string id)
        {
            var memberId = await _crmSystem.LeadFunctionality.QualifyAsync(id.ToGuid());
            return memberId.ToIdString();
        }

        /// <inheritdoc />
        public async Task RejectAsync(string id)
        {
            await _crmSystem.LeadFunctionality.RejectAsync(id.ToGuid(), "Application rejected.");
        }

        /// <inheritdoc />
        public async Task WithdrawAsync(string id)
        {
            await _crmSystem.LeadFunctionality.RejectAsync(id.ToGuid(), "Application withdrawn.");
        }
    }
}

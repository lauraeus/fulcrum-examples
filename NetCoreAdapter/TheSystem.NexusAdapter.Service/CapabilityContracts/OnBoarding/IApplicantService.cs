using System.Collections.Generic;
using System.Threading.Tasks;
using TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding.Model;

namespace TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding
{
    /// <summary>
    /// Methods for dealing with an applicant.
    /// </summary>
    public interface IApplicantService
    {
        /// <summary>
        /// Get a list of all applicants
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Applicant>> ReadAllAsync();

        /// <summary>
        /// Add one application
        /// </summary>
        /// <param name="applicant">Data for the lead</param>
        /// <returns>The id for the newly created customer</returns>
        Task<string> CreateAsync(Applicant applicant);

        /// <summary>
        /// Approve an application to become a member
        /// </summary>
        /// <param name="id">The id of the lead to qualify.</param>
        /// <returns>The internal id of the new customer record.</returns>
        Task<string> ApproveAsync(string id);

        /// <summary>
        /// Deny an applicant to become member
        /// </summary>
        /// <param name="id">The id of the lead to qualify.</param>
        Task RejectAsync(string id);

        /// <summary>
        /// The applicant has withdrawn the application
        /// </summary>
        /// <param name="id">The id of the applicant.</param>
        Task WithdrawAsync(string id);
    }
}
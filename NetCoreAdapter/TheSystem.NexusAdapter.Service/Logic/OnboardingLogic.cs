using TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.Logic
{
    public class OnBoardingLogic : IOnBoardingService
    {
        public OnBoardingLogic(ICrmSystem system)
        {
            ApplicantService = new ApplicantLogic(system);
            MemberService = new MemberLogic(system);
        }

        /// <inheritdoc />
        public IApplicantService ApplicantService { get; }

        /// <inheritdoc />
        public IMemberService MemberService { get; }
    }
}

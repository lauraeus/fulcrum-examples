using TheSystem.NexusAdapter.Service.Projects.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.Projects.AdapterLogic.OnBoarding
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

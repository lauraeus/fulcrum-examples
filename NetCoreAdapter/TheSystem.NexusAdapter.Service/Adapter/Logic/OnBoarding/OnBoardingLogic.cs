using TheSystem.NexusAdapter.Service.NexusApi.CapabilityContracts.OnBoarding;
using TheSystem.NexusAdapter.Service.System.CrmSystemContract;

namespace TheSystem.NexusAdapter.Service.Adapter.Logic.OnBoarding
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

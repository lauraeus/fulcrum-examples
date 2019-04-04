using TheSystem.NexusAdapter.Service.Projects.CapabilityContracts.OnBoarding.Model;
using TheSystem.NexusAdapter.Service.Projects.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.Projects.AdapterLogic.OnBoarding
{
    public static class TypeExtensions
    {
        public static Applicant From(this Applicant target, Lead source)
        {
            target.Name = source.Name;
            target.Id = source.Id.ToIdString();
            return target;
        }
        public static Member From(this Member target, Contact source)
        {
            target.Name = source.Name;
            target.MembershipNumber = source.CustomerNumber;
            target.Id = source.Id.ToIdString();
            return target;
        }

        public static Lead From(this Lead target, Applicant source)
        {
            target.Name = source.Name;
            return target;
        }
    }
}

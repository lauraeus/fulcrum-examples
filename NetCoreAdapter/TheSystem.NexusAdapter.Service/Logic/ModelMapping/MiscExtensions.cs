using System;
using Nexus.Link.Libraries.Core.Assert;
using TheSystem.NexusAdapter.Service.CapabilityContracts.OnBoarding.Model;
using TheSystem.NexusAdapter.Service.CrmSystemContract.Model;

namespace TheSystem.NexusAdapter.Service.Logic.ModelMapping
{
    public static class MiscExtensions
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

        public static string ToIdString(this Guid source)
        {
            return source.ToString().ToLowerInvariant();
        }

        public static Guid ToGuid(this string source)
        {
            InternalContract.Require(Guid.TryParse(source, out var target), nameof(source));
            return target;
        }
    }
}

using System;
using Nexus.Link.Libraries.Core.Assert;

namespace TheSystem.NexusAdapter.Service.Adapter.Logic
{
    public static class TypeExtensions
    {
        public static string ToIdString(this Guid source)
        {
            return source.ToString().ToLowerInvariant();
        }

        public static Guid ToGuid(this string source)
        {
            InternalContract.Require(Guid.TryParse(source, out var target), nameof(source));
            return target;
        }

        public static string ToIso8061Time(this DateTimeOffset time)
        {
            return time.ToString("O");
        }
    }
}

using System;
using Frobozz.PersonProfiles.Dal.Contracts.PersonProfile;
using Xlent.Lever.Libraries2.Standard.Assert;
using SM = Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;

namespace Frobozz.PersonProfiles.Bll
{
    public static class Mapping
    {
        public static StorablePersonProfile ToStorage(SM.IPersonProfile source)
        {
            if (source == null) return null;
            var target = new StorablePersonProfile
            {
                Id = ToGuid(source.Id),
                ETag = source.ETag,
                GivenName = source.GivenName,
                Surname = source.Surname
            };
            return target;
        }
        public static SM.IPersonProfile ToService(StorablePersonProfile source)
        {
            if (source == null) return null;
            var target = new SM.PersonProfile()
            {
                Id = source.Id.ToString(),
                ETag = source.ETag,
                GivenName = source.GivenName,
                Surname = source.Surname
            };
            return target;
        }

        private static Guid ToGuid(string id)
        {
#pragma warning disable IDE0018 // Inline variable declaration
            Guid guid;
#pragma warning restore IDE0018 // Inline variable declaration
            InternalContract.Require(Guid.TryParse(id, out guid), $"Expected a Guid in {nameof(id)} but the value was ({id}).");
            return guid;
        }
    }
}

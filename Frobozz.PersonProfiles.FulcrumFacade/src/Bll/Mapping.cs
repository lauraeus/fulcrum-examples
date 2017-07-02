using System;
using Xlent.Lever.Libraries2.Standard.Assert;
using DM = Frobozz.PersonProfiles.Dal.MemoryStorage.PersonProfile;
using SM = Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;

namespace Frobozz.PersonProfiles.Bll
{
    public static class Mapping
    {
        public static DM.StorablePersonProfile ToStorage(SM.PersonProfile source)
        {
            if (source == null) return null;
            Guid guid;
            InternalContract.Require(Guid.TryParse(source.Id, out guid), $"Expected a Guid for {nameof(source.Id)} ({source.Id}");
            var target = new DM.StorablePersonProfile
            {
                Id = guid,
                ETag = source.ETag,
                GivenName = source.GivenName,
                Surname = source.Surname
            };
            return target;
        }
        public static SM.PersonProfile ToService(DM.StorablePersonProfile source)
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
    }
}

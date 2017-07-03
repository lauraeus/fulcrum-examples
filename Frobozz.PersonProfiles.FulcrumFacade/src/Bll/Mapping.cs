using System;
using Frobozz.PersonProfiles.Dal.Contracts.PersonProfile;
using Xlent.Lever.Libraries2.Standard.Assert;
using SM = Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;

namespace Frobozz.PersonProfiles.Bll
{
    public static class Mapping
    {
        public static StorablePersonProfile ToStorage(SM.PersonProfile source)
        {
            if (source == null) return null;
            Guid guid;
            InternalContract.Require(Guid.TryParse(source.Id, out guid), $"Expected a Guid for {nameof(source.Id)} ({source.Id}");
            var target = new StorablePersonProfile
            {
                Id = guid,
                ETag = source.ETag,
                GivenName = source.GivenName,
                Surname = source.Surname
            };
            return target;
        }
        public static SM.PersonProfile ToService(StorablePersonProfile source)
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

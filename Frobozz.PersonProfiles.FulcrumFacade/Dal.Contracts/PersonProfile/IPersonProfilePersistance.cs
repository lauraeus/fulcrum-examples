using System;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Frobozz.PersonProfiles.Dal.Contracts.PersonProfile
{
    /// <summary>
    /// Storage for person profiles
    /// </summary>
    public interface IPersonProfilePersistance<T> : ICrud<T, Guid>
        where T : IStorablePersonProfile
    {
    }
}

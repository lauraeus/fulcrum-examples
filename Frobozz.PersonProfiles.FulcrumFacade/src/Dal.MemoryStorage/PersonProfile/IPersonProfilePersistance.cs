using System;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Frobozz.PersonProfiles.Dal.MemoryStorage.PersonProfile
{
    /// <summary>
    /// Storage for person profiles
    /// </summary>
    public interface IPersonProfilePersistance : ICrud<IStorableItem<Guid>, Guid>
    {
    }
}

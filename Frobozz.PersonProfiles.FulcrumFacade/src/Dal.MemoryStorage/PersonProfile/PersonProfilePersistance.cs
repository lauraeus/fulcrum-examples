using Xlent.Lever.Libraries2.Standard.Storage.Logic;

namespace Frobozz.PersonProfiles.Dal.MemoryStorage.PersonProfile
{
    /// <summary>
    /// Persistance for <see cref="StorablePersonProfile"/>
    /// </summary>
    public class PersonProfilePersistance : MemoryStorage<StorablePersonProfile>, IPersonProfilePersistance
    {
    }
}

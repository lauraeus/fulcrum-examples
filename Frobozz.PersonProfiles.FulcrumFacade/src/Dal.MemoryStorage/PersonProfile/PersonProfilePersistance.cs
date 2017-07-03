using Frobozz.PersonProfiles.Dal.Contracts.PersonProfile;
using Xlent.Lever.Libraries2.Standard.Storage.Logic;

namespace Frobozz.PersonProfiles.Dal.MemoryStorage.PersonProfile
{
    /// <summary>
    /// Persistance for <see cref="PersonProfile"/>
    /// </summary>
    public class PersonProfilePersistance : MemoryStorage<PersonProfile>, IPersonProfilePersistance<PersonProfile>
    {
    }
}

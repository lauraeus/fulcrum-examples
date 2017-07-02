using System.Threading.Tasks;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;

namespace Frobozz.PersonProfiles.Bll
{
    public interface IPersonProfilesFunctionality : IPersonProfilesService
    {
        Task<Location> GeocodeAsync(Address address, bool mustBeUnique);
    }
}
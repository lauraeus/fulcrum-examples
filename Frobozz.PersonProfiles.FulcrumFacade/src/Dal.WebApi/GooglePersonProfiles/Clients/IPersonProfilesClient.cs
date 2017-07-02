using System.Threading.Tasks;
using Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Models;

namespace Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Clients
{
    public interface IPersonProfilesClient
    {
        Task<PersonProfilesResponse> GeocodeAsync(Address address);
    }
}

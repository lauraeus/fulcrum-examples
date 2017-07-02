using System.Threading.Tasks;

namespace Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles
{
    /// <summary>
    /// Services for PersonProfiles
    /// </summary>
    public interface IPersonProfilesService
    {
        /// <summary>
        /// Geocode an <paramref name="address"/> into a longitude + latitude <see cref="Location"/>.
        /// </summary>
        Task<Location> GeocodeAsync(Address address);
    }
}
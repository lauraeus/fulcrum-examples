using System.Threading.Tasks;

namespace Frobozz.Geocoding.FulcrumFacade.Contract.Geocoding
{
    /// <summary>
    /// Services for geocoding
    /// </summary>
    public interface IGeocodingService
    {
        /// <summary>
        /// Geocode an <paramref name="address"/> into a longitude + latitude <see cref="Location"/>.
        /// </summary>
        Task<Location> GeocodeAsync(Address address);
    }
}
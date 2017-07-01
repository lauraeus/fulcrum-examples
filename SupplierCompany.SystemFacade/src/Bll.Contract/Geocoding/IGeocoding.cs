using System.Threading.Tasks;

namespace SupplierCompany.SystemFacade.Fulcrum.Contract.Geocoding
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
using System.Threading.Tasks;
using Frobozz.Geocoding.Dal.WebApi.GoogleGeocode.Models;

namespace Frobozz.Geocoding.Dal.WebApi.GoogleGeocode.Clients
{
    public interface IGeocodeClient
    {
        Task<GeocodingResponse> GeocodeAsync(Address address);
    }
}

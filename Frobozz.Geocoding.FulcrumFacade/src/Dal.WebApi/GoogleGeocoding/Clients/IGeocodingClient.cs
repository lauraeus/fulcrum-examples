using System.Threading.Tasks;
using Frobozz.Geocoding.Dal.WebApi.GoogleGeocoding.Models;

namespace Frobozz.Geocoding.Dal.WebApi.GoogleGeocoding.Clients
{
    public interface IGeocodingClient
    {
        Task<GeocodingResponse> GeocodeAsync(Address address);
    }
}

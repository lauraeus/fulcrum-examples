using System.Threading.Tasks;
using Frobozz.FulcrumFacade.Dal.WebApi.GoogleGeocode.Models;

namespace Frobozz.FulcrumFacade.Dal.WebApi.GoogleGeocode.Clients
{
    public interface IGeocodeClient
    {
        Task<GeocodingResponse> GeocodeAsync(Address address);
    }
}

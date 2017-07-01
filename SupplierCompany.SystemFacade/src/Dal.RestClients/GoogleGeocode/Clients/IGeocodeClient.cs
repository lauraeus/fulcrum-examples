using System.Threading.Tasks;
using SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Models;

namespace SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Clients
{
    public interface IGeocodeClient
    {
        Task<GeocodingResponse> GeocodeAsync(Address address);
    }
}

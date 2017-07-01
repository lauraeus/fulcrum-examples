using System.Threading.Tasks;
using SupplierCompany.SystemFacade.Dal.WebApi.GoogleGeocode.Models;

namespace SupplierCompany.SystemFacade.Dal.WebApi.GoogleGeocode.Clients
{
    public interface IGeocodeClient
    {
        Task<GeocodingResponse> GeocodeAsync(Address address);
    }
}

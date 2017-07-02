using System.Threading.Tasks;
using Frobozz.Geocoding.FulcrumFacade.Contract.Geocoding;

namespace Frobozz.Geocoding.Bll
{
    public interface IGeocodingFunctionality : IGeocodingService
    {
        Task<Location> GeocodeAsync(Address address, bool mustBeUnique);
    }
}
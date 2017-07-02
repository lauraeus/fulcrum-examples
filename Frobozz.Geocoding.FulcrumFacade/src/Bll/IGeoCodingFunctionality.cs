using System.Threading.Tasks;
using Frobozz.FulcrumFacade.Fulcrum.Contract.Geocoding;

namespace Frobozz.FulcrumFacade.Bll
{
    public interface IGeocodingFunctionality : IGeocodingService
    {
        Task<Location> GeocodeAsync(Address address, bool mustBeUnique);
    }
}
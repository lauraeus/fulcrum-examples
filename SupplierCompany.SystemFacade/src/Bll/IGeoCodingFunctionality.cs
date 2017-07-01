using System.Threading.Tasks;
using SupplierCompany.SystemFacade.Fulcrum.Contract;

namespace SupplierCompany.SystemFacade.Bll
{
    public interface IGeocodingFunctionality : IGeocodingService
    {
        Task<Location> GeocodeAsync(Address address, bool mustBeUnique);
    }
}
using System.Threading.Tasks;

namespace Frobozz.CapabilityContracts.Core.Mapping
{
    public interface IMapper<TModel, in TLogic>
    {
        Task MapFromAsync(TModel source, TLogic logic);

        Task<TModel> CreateAndMapTo(TLogic logic);
    }
}

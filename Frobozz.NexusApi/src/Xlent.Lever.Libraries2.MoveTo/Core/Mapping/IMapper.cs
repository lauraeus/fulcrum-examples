using System.Threading;
using System.Threading.Tasks;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    public interface IMapper<TModel, in TLogic>
    {
        Task MapFromAsync(TModel source, TLogic logic, CancellationToken token = default(CancellationToken));

        Task<TModel> CreateAndMapToAsync(TLogic logic, CancellationToken token = default(CancellationToken));
    }
}

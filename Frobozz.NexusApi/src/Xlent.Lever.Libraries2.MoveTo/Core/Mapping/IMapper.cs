using System.Threading;
using System.Threading.Tasks;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    public interface IMapper<TClientModel, in TLogic, TServerModel>
    {
        Task<TClientModel> CreateAndMapFromServerAsync(TServerModel source, TLogic logic, CancellationToken token = default(CancellationToken));

        Task<TServerModel> CreateAndMapToServerAsync(TClientModel source, TLogic logic, CancellationToken token = default(CancellationToken));
    }
}

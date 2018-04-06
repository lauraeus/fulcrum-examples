using System.Threading;
using System.Threading.Tasks;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    public interface IMapper<TClientModel, in TServerLogic, TServerModel>
    {
        Task<TClientModel> CreateAndMapFromServerAsync(TServerModel source, TServerLogic logic, CancellationToken token = default(CancellationToken));

        Task<TServerModel> CreateAndMapToServerAsync(TClientModel source, TServerLogic logic, CancellationToken token = default(CancellationToken));
    }
}

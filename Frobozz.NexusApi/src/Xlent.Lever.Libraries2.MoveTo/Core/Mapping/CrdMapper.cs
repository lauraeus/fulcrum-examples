using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    /// <summary>
    /// Mapping for ICrd.
    /// </summary>
    public class CrdMapper<TClientModel, TClientId, TLogic, TServerModel, TServerId> : ReadMapper<TClientModel, TClientId, TLogic, TServerModel, TServerId>, ICrd<TClientModel, TClientId>
    where TClientModel : IMapper<TServerModel, TLogic>, new()
    {
        private readonly ICrd<TServerModel, TServerId> _server;
        /// <summary>
        /// Constructor 
        /// </summary>
        public CrdMapper(ICrd<TServerModel, TServerId> server, TLogic logic)
        :base(server, logic)
        {
            _server = server;
        }

        /// <inheritdoc />
        public virtual async Task<TClientId> CreateAsync(TClientModel item, CancellationToken token = default(CancellationToken))
        {
            var serverItem = await item.CreateAndMapToAsync(Logic, token);
            var serverId = await _server.CreateAsync(serverItem, token);
            return MapToClientId(serverId);
        }

        /// <inheritdoc />
        public virtual async Task<TClientModel> CreateAndReturnAsync(TClientModel item, CancellationToken token = default(CancellationToken))
        {
            var serverItem = await item.CreateAndMapToAsync(Logic, token);
            serverItem = await _server.CreateAndReturnAsync(serverItem, token);
            return await MapToClientAsync(serverItem, token);
        }

        /// <inheritdoc />
        public virtual async Task CreateWithSpecifiedIdAsync(TClientId id, TClientModel item, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(id);
            var serverItem = await item.CreateAndMapToAsync(Logic, token);
            await _server.CreateWithSpecifiedIdAsync(serverId, serverItem, token);
        }

        /// <inheritdoc />
        public virtual async Task<TClientModel> CreateWithSpecifiedIdAndReturnAsync(TClientId id, TClientModel item, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(id);
            var serverItem = await item.CreateAndMapToAsync(Logic, token);
            serverItem = await _server.CreateWithSpecifiedIdAndReturnAsync(serverId, serverItem, token);
            return await MapToClientAsync(serverItem, token);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(TClientId id, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(id);
            await _server.DeleteAsync(serverId, token);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAllAsync(CancellationToken token = default(CancellationToken))
        {
            await _server.DeleteAllAsync(token);
        }
    }
}

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
        public virtual async Task<TClientId> CreateAsync(TClientModel item)
        {
            var serverItem = await item.CreateAndMapTo(Logic);
            var serverId = await _server.CreateAsync(serverItem);
            return MapToClientId(serverId);
        }

        /// <inheritdoc />
        public virtual async Task<TClientModel> CreateAndReturnAsync(TClientModel item)
        {
            var serverItem = await item.CreateAndMapTo(Logic);
            serverItem = await _server.CreateAndReturnAsync(serverItem);
            return await MapToClientAsync(serverItem);
        }

        /// <inheritdoc />
        public virtual async Task CreateWithSpecifiedIdAsync(TClientId id, TClientModel item)
        {
            var serverId = MapToServerId(id);
            var serverItem = await item.CreateAndMapTo(Logic);
            await _server.CreateWithSpecifiedIdAsync(serverId, serverItem);
        }

        /// <inheritdoc />
        public virtual async Task<TClientModel> CreateWithSpecifiedIdAndReturnAsync(TClientId id, TClientModel item)
        {
            var serverId = MapToServerId(id);
            var serverItem = await item.CreateAndMapTo(Logic);
            serverItem = await _server.CreateWithSpecifiedIdAndReturnAsync(serverId, serverItem);
            return await MapToClientAsync(serverItem);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(TClientId id)
        {
            var serverId = MapToServerId(id);
            await _server.DeleteAsync(serverId);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAllAsync()
        {
            await _server.DeleteAllAsync();
        }
    }
}

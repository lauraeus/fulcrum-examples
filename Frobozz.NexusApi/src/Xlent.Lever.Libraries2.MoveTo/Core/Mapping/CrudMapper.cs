using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    /// <summary>
    /// Mapping for ICrud.
    /// </summary>
    public class CrudMapper<TClientModel, TClientId, TLogic, TServerModel, TServerId> : CrdMapper<TClientModel, TClientId, TLogic, TServerModel, TServerId>, ICrud<TClientModel, TClientId>
    where TClientModel : IMapper<TServerModel, TLogic>, new()
    {
        private readonly ICrud<TServerModel, TServerId> _server;
        /// <summary>
        /// Constructor 
        /// </summary>
        public CrudMapper(ICrud<TServerModel, TServerId> server, TLogic logic)
        :base(server, logic)
        {
            _server = server;
        }

        /// <inheritdoc />
        public virtual async Task UpdateAsync(TClientId id, TClientModel item, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapHelper.MapId<TServerId, TClientId>(id);
            var serverItem = await item.CreateAndMapToAsync(Logic, token);
            await _server.UpdateAsync(serverId, serverItem, token);
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("{id}")]
        public virtual async Task<TClientModel> UpdateAndReturnAsync(TClientId id, TClientModel item, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapHelper.MapId<TServerId, TClientId>(id);
            var serverItem = await item.CreateAndMapToAsync(Logic, token);
            serverItem = await _server.UpdateAndReturnAsync(serverId, serverItem, token);
            return await MapToClientAsync(serverItem, token);
        }
    }
}

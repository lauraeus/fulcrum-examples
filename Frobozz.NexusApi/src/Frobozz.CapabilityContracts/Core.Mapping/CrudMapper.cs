using System;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Core.Mapping
{
    /// <summary>
    /// Logic for Product. 
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
        public async Task UpdateAsync(TClientId id, TClientModel item)
        {
            var serverId = MapHelper.MapId<TServerId, TClientId>(id);
            var serverItem = await item.CreateAndMapTo(Logic);
            await _server.UpdateAsync(serverId, serverItem);
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("{id}")]
        public async Task<TClientModel> UpdateAndReturnAsync(TClientId id, TClientModel item)
        {
            var serverId = MapHelper.MapId<TServerId, TClientId>(id);
            var serverItem = await item.CreateAndMapTo(Logic);
            serverItem = await _server.UpdateAndReturnAsync(serverId, serverItem);
            return await MapToClientAsync(serverItem);
        }
    }
}

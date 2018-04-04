using System.Collections.Generic;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Core.Mapping
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class ReadMapper<TClientModel, TClientId, TLogic, TServerModel, TServerId> : MapperBase<TClientModel, TClientId, TLogic, TServerModel, TServerId>, IReadAll<TClientModel, TClientId>
    where TClientModel : IMapper<TServerModel, TLogic>, new()
    {
        private readonly IReadAll<TServerModel, TServerId> _server;
        
        /// <summary>
        /// Constructor 
        /// </summary>
        public ReadMapper(IReadAll<TServerModel, TServerId> server, TLogic logic)
        :base(logic)
        {
            _server = server;
        }

        /// <inheritdoc />
        public async Task<TClientModel> ReadAsync(TClientId id)
        {
            var serverId = MapToServerId(id);
            var serverItem = await _server.ReadAsync(serverId);
            return await MapToClientAsync(serverItem);
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<TClientModel>> ReadAllWithPagingAsync(int offset, int? limit = null)
        {
            var serverPage = await _server.ReadAllWithPagingAsync(offset, limit);
            FulcrumAssert.IsNotNull(serverPage);
            return new PageEnvelope<TClientModel>(serverPage.PageInfo, await MapToClientAsync(serverPage.Data));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TClientModel>> ReadAllAsync(int limit = int.MaxValue)
        {
            var serverItems = await _server.ReadAllAsync(limit);
            return await MapToClientAsync(serverItems);
        }
    }
}

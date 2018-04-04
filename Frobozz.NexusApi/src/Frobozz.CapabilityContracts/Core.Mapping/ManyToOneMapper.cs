using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.Core.Mapping
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class ManyToOneMapper<TClientModel, TClientId, TLogic, TServerModel, TServerId> : MapperBase<TClientModel, TClientId, TLogic, TServerModel, TServerId>, IManyToOneRelation<TClientModel, TClientId>
    where TClientModel : IMapper<TServerModel, TLogic>, new()
    {
        private readonly IManyToOneRelation<TServerModel, TServerId> _server;
        /// <summary>
        /// Constructor 
        /// </summary>
        public ManyToOneMapper(IManyToOneRelation<TServerModel, TServerId> server, TLogic logic)
        :base(logic)
        {
            _server = server;
        }

        /// h<inheritdoc />
        public async Task<PageEnvelope<TClientModel>> ReadChildrenWithPagingAsync(TClientId parentId, int offset, int? limit = null)
        {
            var serverId = MapToServerId(parentId);
            var serverPage = await _server.ReadChildrenWithPagingAsync(serverId, offset, limit);
            FulcrumAssert.IsNotNull(serverPage);
            return new PageEnvelope<TClientModel>(serverPage.PageInfo, await MapToClientAsync(serverPage.Data));
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}/Consents")]
        public async Task<IEnumerable<TClientModel>> ReadChildrenAsync(TClientId parentId, int limit = int.MaxValue)
        {
            var serverId = MapToServerId(parentId);
            var serverItems = await _server.ReadChildrenAsync(serverId, limit);
            return await MapToClientAsync(serverItems);
        }

        /// <inheritdoc />
        public async Task DeleteChildrenAsync(TClientId parentId)
        {
            var serverId = MapToServerId(parentId);
            await _server.DeleteChildrenAsync(serverId);
        }
    }
}

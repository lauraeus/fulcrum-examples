using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    /// <summary>
    /// Mapping for IReadAll.
    /// </summary>
    public class ReadMapper<TClientModel, TClientId, TLogic, TServerModel, TServerId> : MapperBase<TClientModel, TClientId, TLogic, TServerModel, TServerId>, IReadAll<TClientModel, TClientId>
    {
        private readonly IReadAll<TServerModel, TServerId> _server;
        
        /// <summary>
        /// Constructor 
        /// </summary>
        public ReadMapper(IReadAll<TServerModel, TServerId> server, TLogic logic, IMapper<TClientModel, TLogic, TServerModel> mapper)
        :base(logic, mapper)
        {
            _server = server;
        }

        /// <inheritdoc />
        public virtual async Task<TClientModel> ReadAsync(TClientId id, CancellationToken token = default(CancellationToken))
        {
            var serverId = MapToServerId(id);
            var serverItem = await _server.ReadAsync(serverId, token);
            return await CreateAndMapFromServerAsync(serverItem, token);
        }

        /// <inheritdoc />
        public virtual async Task<PageEnvelope<TClientModel>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            var serverPage = await _server.ReadAllWithPagingAsync(offset, limit, token);
            FulcrumAssert.IsNotNull(serverPage);
            return new PageEnvelope<TClientModel>(serverPage.PageInfo, await CreateAndMapFromServerAsync(serverPage.Data, token));
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<TClientModel>> ReadAllAsync(int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            var serverItems = await _server.ReadAllAsync(limit, token);
            return await CreateAndMapFromServerAsync(serverItems, token);
        }
    }
}

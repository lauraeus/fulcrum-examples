using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
{
    /// <summary>
    /// Base class for the mapper classes
    /// </summary>
    /// <typeparam name="TClientModel"></typeparam>
    /// <typeparam name="TClientId"></typeparam>
    /// <typeparam name="TServerLogic"></typeparam>
    /// <typeparam name="TServerModel"></typeparam>
    /// <typeparam name="TServerId"></typeparam>
    public abstract class MapperBase<TClientModel, TClientId, TServerLogic, TServerModel, TServerId>
    {
        protected TServerLogic Storage { get; }
        public IMapper<TClientModel, TServerLogic, TServerModel> Mapper { get; }

        /// <summary>
        /// Constructor 
        /// </summary>
        protected MapperBase(TServerLogic storage, IMapper<TClientModel, TServerLogic, TServerModel> mapper)
        {
            Storage = storage;
            Mapper = mapper;
        }

        protected async Task<TClientModel[]> CreateAndMapFromServerAsync(IEnumerable<TServerModel> serverItems, CancellationToken token = default(CancellationToken))
        {
            if (serverItems == null) return null;
            var clientItemTasks = serverItems.Select(async si => await CreateAndMapFromServerAsync(si, token));
            return await Task.WhenAll(clientItemTasks);
        }

        protected async Task<TClientModel> CreateAndMapFromServerAsync(TServerModel serverItem, CancellationToken token = default(CancellationToken))
        {
            return await Mapper.CreateAndMapFromServerAsync(serverItem, Storage, token);
        }

        protected async Task<TServerModel> CreateAndMapToServerAsync(TClientModel clientItem, CancellationToken token = default(CancellationToken))
        {
            return await Mapper.CreateAndMapToServerAsync(clientItem, Storage, token);
        }

        protected static TClientId MapToClientId(TServerId id)
        {
            return MapperHelper.MapId<TClientId, TServerId>(id);
        }

        protected static TServerId MapToServerId(TClientId id)
        {
            return MapperHelper.MapId<TServerId, TClientId>(id);
        }
    }
}

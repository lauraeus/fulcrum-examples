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
        protected TServerLogic ServerLogic { get; }
        public IModelMapper<TClientModel, TServerLogic, TServerModel> ModelMapper { get; }

        /// <summary>
        /// Constructor 
        /// </summary>
        protected MapperBase(TServerLogic serverLogic, IModelMapper<TClientModel, TServerLogic, TServerModel> modelMapper)
        {
            ServerLogic = serverLogic;
            ModelMapper = modelMapper;
        }

        protected async Task<TClientModel[]> CreateAndMapFromServerAsync(IEnumerable<TServerModel> serverItems, CancellationToken token = default(CancellationToken))
        {
            if (serverItems == null) return null;
            var clientItemTasks = serverItems.Select(async si => await CreateAndMapFromServerAsync(si, token));
            return await Task.WhenAll(clientItemTasks);
        }

        protected async Task<TClientModel> CreateAndMapFromServerAsync(TServerModel serverItem, CancellationToken token = default(CancellationToken))
        {
            return await ModelMapper.CreateAndMapFromServerAsync(serverItem, ServerLogic, token);
        }

        protected async Task<TServerModel> CreateAndMapToServerAsync(TClientModel clientItem, CancellationToken token = default(CancellationToken))
        {
            return await ModelMapper.CreateAndMapToServerAsync(clientItem, ServerLogic, token);
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

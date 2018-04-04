using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frobozz.CapabilityContracts.Core.Mapping
{
    /// <summary>
    /// Base class for the mapper classes
    /// </summary>
    /// <typeparam name="TClientModel"></typeparam>
    /// <typeparam name="TClientId"></typeparam>
    /// <typeparam name="TLogic"></typeparam>
    /// <typeparam name="TServerModel"></typeparam>
    /// <typeparam name="TServerId"></typeparam>
    public abstract class MapperBase<TClientModel, TClientId, TLogic, TServerModel, TServerId>
    where TClientModel : IMapper<TServerModel, TLogic>, new()
    {
        protected TLogic Logic { get; }
        
        /// <summary>
        /// Constructor 
        /// </summary>
        protected MapperBase(TLogic logic)
        {
            Logic = logic;
        }

        protected async Task<TClientModel[]> MapToClientAsync(IEnumerable<TServerModel> serverItems)
        {
            if (serverItems == null) return null;
            var clientItemTasks = serverItems.Select(async si => await MapToClientAsync(si));
            return await Task.WhenAll(clientItemTasks);
        }

        protected async Task<TClientModel> MapToClientAsync(TServerModel serverItem)
        {
            return await MapHelper.CreateAndMapToAsync<TClientModel, TServerModel, TLogic>(serverItem, Logic);
        }

        protected static TClientId MapToClientId(TServerId id)
        {
            return MapHelper.MapId<TClientId, TServerId>(id);
        }

        protected static TServerId MapToServerId(TClientId id)
        {
            return MapHelper.MapId<TServerId, TClientId>(id);
        }
    }
}

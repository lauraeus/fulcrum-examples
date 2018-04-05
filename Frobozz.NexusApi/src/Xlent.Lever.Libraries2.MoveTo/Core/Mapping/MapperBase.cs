using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Error.Logic;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Mapping
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
    {
        protected TLogic Logic { get; }
        public IMapper<TClientModel, TLogic, TServerModel> Mapper { get; }

        /// <summary>
        /// Constructor 
        /// </summary>
        protected MapperBase(TLogic logic, IMapper<TClientModel, TLogic, TServerModel> mapper)
        {
            Logic = logic;
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
            return await Mapper.CreateAndMapFromServerAsync(serverItem, Logic, token);
        }

        protected async Task<TServerModel> CreateAndMapToServerAsync(TClientModel clientItem, CancellationToken token = default(CancellationToken))
        {
            return await Mapper.CreateAndMapToServerAsync(clientItem, Logic, token);
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

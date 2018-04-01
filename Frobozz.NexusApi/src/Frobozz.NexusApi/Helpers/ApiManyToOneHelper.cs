using System.Collections.Generic;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.NexusApi.Helpers
{
    public class ApiManyToOneHelper<TModel> : IManyToOneRelation<TModel, string>
    {
        private readonly IManyToOneRelation<TModel, string> _storage;

        public ApiManyToOneHelper(IManyToOneRelation<TModel, string> storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<TModel>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null)
        {
            ServiceContract.RequireNotNullOrWhitespace(parentId, nameof(parentId));
            ServiceContract.RequireGreaterThanOrEqualTo(0, offset, nameof(offset));
            if (limit != null)
            {
                ServiceContract.RequireGreaterThan(0, limit.Value, nameof(limit));
            }
            return await _storage.ReadChildrenWithPagingAsync(parentId, offset, limit);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TModel>> ReadChildrenAsync(string parentId, int limit = int.MaxValue)
        {
            ServiceContract.RequireNotNullOrWhitespace(parentId, nameof(parentId));
            ServiceContract.RequireGreaterThan(0, limit, nameof(limit));
            return await _storage.ReadChildrenAsync(parentId, limit);
        }

        /// <inheritdoc />
        public async Task DeleteChildrenAsync(string parentId)
        {
            ServiceContract.RequireNotNullOrWhitespace(parentId, nameof(parentId));
            await _storage.DeleteChildrenAsync(parentId);
        }
    }
}
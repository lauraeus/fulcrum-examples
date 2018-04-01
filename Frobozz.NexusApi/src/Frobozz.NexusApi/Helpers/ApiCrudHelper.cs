using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.NexusApi.Helpers
{
    public class ApiCrudHelper<TModel> : ICrud<TModel, string>
    {
        private ICrud<TModel, string> _storage;

        public ApiCrudHelper(ICrud<TModel, string> storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<string> CreateAsync(TModel item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<TModel> CreateAndReturnAsync(TModel item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task CreateWithSpecifiedIdAsync(string id, TModel item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<TModel> CreateWithSpecifiedIdAndReturnAsync(string id, TModel item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<TModel> ReadAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            return await _storage.ReadAsync(id);
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<TModel>> ReadAllWithPagingAsync(int offset, int? limit = null)
        {
            ServiceContract.RequireGreaterThanOrEqualTo(0, offset, nameof(offset));
            if (limit != null)
            {
                ServiceContract.RequireGreaterThan(0, limit.Value, nameof(limit));
            }

            return await _storage.ReadAllWithPagingAsync(offset, limit);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TModel>> ReadAllAsync(int limit = int.MaxValue)
        {
            ServiceContract.RequireGreaterThan(0, limit, nameof(limit));
            return await _storage.ReadAllAsync(limit);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, TModel item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<TModel> UpdateAndReturnAsync(string id, TModel item)
        {
            throw new NotImplementedException();
        }
    }
}
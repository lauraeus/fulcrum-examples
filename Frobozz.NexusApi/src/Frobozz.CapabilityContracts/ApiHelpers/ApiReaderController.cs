using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.CapabilityContracts.ApiHelpers
{
    public abstract class ApiReaderController<TModel> : ApiController, IReadAll<TModel, string>
    where TModel : IValidatable
    {
        private readonly IReadAll<TModel, string> _storage;

        public ApiReaderController(IReadAll<TModel, string> storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<TModel> ReadAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            return await _storage.ReadAsync(id);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("WithPaging")]
        public virtual async Task<PageEnvelope<TModel>> ReadAllWithPagingAsync(int offset, int? limit = null)
        {
            ServiceContract.RequireGreaterThanOrEqualTo(0, offset, nameof(offset));
            if (limit != null)
            {
                ServiceContract.RequireGreaterThan(0, limit.Value, nameof(limit));
            }

            return await _storage.ReadAllWithPagingAsync(offset, limit);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public virtual async Task<IEnumerable<TModel>> ReadAllAsync(int limit = int.MaxValue)
        {
            ServiceContract.RequireGreaterThan(0, limit, nameof(limit));
            return await _storage.ReadAllAsync(limit);
        }
    }
}
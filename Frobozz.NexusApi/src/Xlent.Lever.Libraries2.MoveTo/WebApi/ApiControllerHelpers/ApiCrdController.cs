using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.WebApi.ApiControllerHelpers
{
    public abstract class ApiCrdController<TModel> : ApiReadController<TModel>, ICrd<TModel, string>
    where TModel : IValidatable
    {
        private readonly ICrd<TModel, string> _storage;

        public ApiCrdController(ICrd<TModel, string> storage)
        :base(storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("")]
        public virtual async Task<string> CreateAsync(TModel item)
        {
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            return await _storage.CreateAsync(item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("ReturnCreated")]
        public virtual async Task<TModel> CreateAndReturnAsync(TModel item)
        {
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            return await _storage.CreateAndReturnAsync(item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}")]
        public virtual async Task CreateWithSpecifiedIdAsync(string id, TModel item)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            await _storage.CreateWithSpecifiedIdAsync(id, item);
        }

        /// <inheritdoc />
        [HttpPost]
        [Route("{id}/ReturnCreated")]
        public virtual async Task<TModel> CreateWithSpecifiedIdAndReturnAsync(string id, TModel item)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            return await _storage.CreateWithSpecifiedIdAndReturnAsync(id, item);
        }


        /// <inheritdoc />
        [HttpDelete]
        [Route("{id}")]
        public virtual async Task DeleteAsync(string id)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            await _storage.DeleteAsync(id);
        }

        /// <inheritdoc />
        [HttpDelete]
        [Route("{id}/ReturnUpdated")]
        public virtual async Task DeleteAllAsync()
        {
            await _storage.DeleteAllAsync();
        }
    }
}
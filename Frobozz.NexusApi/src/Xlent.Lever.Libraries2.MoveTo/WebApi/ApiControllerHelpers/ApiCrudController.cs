using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.WebApi.ApiControllerHelpers
{
    public abstract class ApiCrudController<TModel> : ApiCrdController<TModel>, ICrud<TModel, string>
    where TModel : IValidatable
    {
        private readonly ICrud<TModel, string> _storage;

        public ApiCrudController(ICrud<TModel, string> storage)
        :base(storage)
        {
            _storage = storage;
        }
        /// <inheritdoc />
        [HttpPut]
        [Route("{id}")]
        public virtual async Task UpdateAsync(string id, TModel item, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            await _storage.UpdateAsync(id, item, token);
        }

        /// <inheritdoc />
        [HttpPut]
        [Route("{id}/ReturnUpdated")]
        public virtual async Task<TModel> UpdateAndReturnAsync(string id, TModel item, CancellationToken token = default(CancellationToken))
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            return await _storage.UpdateAndReturnAsync(id, item, token);
        }
    }
}
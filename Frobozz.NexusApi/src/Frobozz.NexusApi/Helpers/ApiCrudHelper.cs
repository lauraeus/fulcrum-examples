using System.Collections.Generic;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.NexusApi.Helpers
{
    public class ApiCrudHelper<TModel> : ApiCrdHelper<TModel>, ICrud<TModel, string>
    where TModel : IValidatable
    {
        private readonly ICrud<TModel, string> _storage;

        public ApiCrudHelper(ICrud<TModel, string> storage)
        :base(storage)
        {
            _storage = storage;
        }
        /// <inheritdoc />
        public async Task UpdateAsync(string id, TModel item)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            await _storage.UpdateAsync(id, item);
        }

        /// <inheritdoc />
        public async Task<TModel> UpdateAndReturnAsync(string id, TModel item)
        {
            ServiceContract.RequireNotNullOrWhitespace(id, nameof(id));
            ServiceContract.RequireNotNull(item, nameof(item));
            ServiceContract.RequireValidated(item, nameof(item));
            return await _storage.UpdateAndReturnAsync(id, item);
        }
    }
}
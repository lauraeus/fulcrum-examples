using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ServerTranslators
{
    public class ReadServerTranslator<TModel> : ServerTranslatorBase, IReadAll<TModel, string>
    where TModel : IValidatable
    {
        private readonly IReadAll<TModel, string> _storage;

        public ReadServerTranslator(IReadAll<TModel, string> storage, string idConceptName)
        :base(idConceptName)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<TModel> ReadAsync(string id, CancellationToken token = default(CancellationToken))
        {
            var translator = new TranslationHelper(ServerName);
            await translator.Add(id).ExecuteAsync();
            id = translator.Translate(id);
            var result = await _storage.ReadAsync(id, token);
            return translator.DecorateItem(result);   
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("WithPaging")]
        public virtual async Task<PageEnvelope<TModel>> ReadAllWithPagingAsync(int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            var translator = new TranslationHelper(ServerName);
            var result =  await _storage.ReadAllWithPagingAsync(offset, limit, token);
            return translator.DecoratePage(result);
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("")]
        public virtual async Task<IEnumerable<TModel>> ReadAllAsync(int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            var translator = new TranslationHelper(ServerName);
            var result = await _storage.ReadAllAsync(limit, token);
            return translator.DecorateItems(result);
        }
    }
}
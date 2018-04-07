using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ServerTranslators
{
    public class ManyToOneServerTranslator<TModel> : ServerTranslatorBase, IManyToOneRelation<TModel, string>
    where TModel : IValidatable
    {
        private readonly IManyToOneRelation<TModel, string> _storage;

        public ManyToOneServerTranslator(IManyToOneRelation<TModel, string> storage, string idConceptName)
        :base(idConceptName)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<PageEnvelope<TModel>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null,
            CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ServerName);
            await translator.Add(parentId).ExecuteAsync();
            parentId = translator.Translate(parentId);
            var result = await _storage.ReadChildrenWithPagingAsync(parentId, offset, limit, token);
            return translator.DecorateItem(result);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TModel>> ReadChildrenAsync(string parentId, int limit = int.MaxValue, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ServerName);
            await translator.Add(parentId).ExecuteAsync();
            parentId = translator.Translate(parentId);
            var result = await _storage.ReadChildrenAsync(parentId, limit, token);
            return translator.DecorateItems(result);
        }

        /// <inheritdoc />
        public async Task DeleteChildrenAsync(string parentId, CancellationToken token = new CancellationToken())
        {
            var translator = new TranslationHelper(ServerName);
            await translator.Add(parentId).ExecuteAsync();
            parentId = translator.Translate(parentId);
            await _storage.DeleteChildrenAsync(parentId, token);
        }
    }
}
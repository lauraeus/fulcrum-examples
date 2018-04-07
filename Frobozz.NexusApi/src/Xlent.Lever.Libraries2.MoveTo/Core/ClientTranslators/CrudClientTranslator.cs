using System.Threading;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators
{
    public class CrudClientTranslator<TModel> : CrdClientTranslator<TModel>, ICrud<TModel, string>
    where TModel : IValidatable
    {
        private readonly ICrud<TModel, string> _storage;

        public CrudClientTranslator(ICrud<TModel, string> storage, string idConceptName)
        :base(storage, idConceptName)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string id, TModel item, CancellationToken token = new CancellationToken())
        {
            var translator = new Translator(ClientName);
            id = translator.Decorate(IdConceptName, id);
            item = translator.DecorateItem(item);
            await _storage.UpdateAsync(id, item, token);
        }

        /// <inheritdoc />
        public async Task<TModel> UpdateAndReturnAsync(string id, TModel item, CancellationToken token = new CancellationToken())
        {
            var translator = new Translator(ClientName);
            id = translator.Decorate(IdConceptName, id);
            item = translator.DecorateItem(item);
            var decoratedResult = await _storage.UpdateAndReturnAsync(id, item, token);
            await translator.Add(decoratedResult).ExecuteAsync();
            return translator.Translate(decoratedResult);
        }
    }
}
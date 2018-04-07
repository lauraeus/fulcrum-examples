using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Translation
{
    public class TranslationHelper
    {
        private string _clientName;

        public TranslationHelper(string clientName)
        {
            _clientName = clientName;
        }

        public string DecorateId(string conceptName, string value)
        {
            throw new System.NotImplementedException();
        }

        public TranslationHelper Add<TModel>(TModel result)
        {
            // TODO: Find all decorated strings and add them to the translation batch.
            return this;
        }

        public async Task ExecuteAsync()
        {
            // TODO: Send to translation
            await Task.Yield();
        }

        public T Translate<T>(T item)
        {
            if (item == null) return default(T);
            var json = JsonConvert.SerializeObject(item);
            // TODO: Translate
            return JsonConvert.DeserializeObject<T>(json);
        }

        public TModel DecorateItem<TModel>(TModel item) where TModel : IValidatable
        {
            if (item == null) return default(TModel);
            throw new NotImplementedException();
        }

        public IEnumerable<TModel> DecorateItems<TModel>(IEnumerable<TModel> items) where TModel : IValidatable
        {
            if (items == null) return null;
            var array = items as TModel[] ?? items.ToArray();
            foreach (var item in array)
            {
                DecorateItem(item);
            }

            return array;
        }

        public PageEnvelope<TModel> DecoratePage<TModel>(PageEnvelope<TModel> page) where TModel : IValidatable
        {
            if (page == null) return null;
            page.Data = DecorateItems(page.Data);
            return page;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Translation
{
    public class Translator
    {
        private readonly string _clientName;
        private readonly ITranslatorService _service;
        private Dictionary<string, string> _translations;

        public Translator(string clientName,ITranslatorService service)
        {
            _clientName = clientName;
            _service = service;
            _translations = new Dictionary<string, string>();
        }

        public string Decorate(string conceptName, string value)
        {
            return IsDecorated(value) ? value : Decorate(conceptName, _clientName, value);
        }

        public TModel DecorateItem<TModel>(TModel item) where TModel : IValidatable
        {
            if (item == null) return default(TModel);
            if (item is ITranslatable translatable)
            {
                translatable.DecorateForTranslation(this);
            }
            return item;
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

        private bool IsDecorated(string value)
        {
            // TODO: Check if value matches "(!!)"
            return false;
        }

        private static string Decorate(string conceptName, string clientName, string value) =>
            $"({conceptName}!~{clientName}!{value})";

        public Translator Add<T>(T item)
        {
            if (item == null) return this;
            var regex = new Regex(@"\(([^!]+)!([^!]+)!(.+)\)", RegexOptions.Compiled);
            var jsonString = JsonConvert.SerializeObject(item);
            foreach (Match match in regex.Matches(jsonString))
            {
                var conceptPath = match.Groups[0].ToString();
                _translations.Add(conceptPath, null);
            }
            // TODO: Find all decorated strings and add them to the translation batch.
            return this;
        }

        public async Task ExecuteAsync()
        {
            await _service.TranslateAsync(_translations);
        }

        public T Translate<T>(T item)
        {
            if (item == null) return default(T);
            var json = JsonConvert.SerializeObject(item);
            // TODO: Translate
            return JsonConvert.DeserializeObject<T>(json);
        }

        public PageEnvelope<TModel> DecoratePage<TModel>(PageEnvelope<TModel> page) where TModel : IValidatable
        {
            if (page == null) return null;
            page.Data = DecorateItems(page.Data);
            return page;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Test
{
    public class TranslatorServiceMock : ITranslatorService
    {
        /// <inheritdoc />
        public async Task TranslateAsync(IEnumerable<string> conceptValues, IDictionary<string, string> translations)
        {
            foreach (var path in conceptValues)
            {
                if (!ConceptValue.TryParse(path, out var conceptValue))
                {
                    translations[path] = path;
                    continue;
                }
                var value = conceptValue.Value;
                if (conceptValue.Value.Contains("client-")) value = value.Replace("client-", "server-");
                else if (conceptValue.Value.Contains("server-")) value = value.Replace("server-", "client-");
                translations[path] = value;
            }
            await Task.Yield();
        }
    }
}

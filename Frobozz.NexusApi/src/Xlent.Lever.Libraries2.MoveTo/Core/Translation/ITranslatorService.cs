using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xlent.Lever.Libraries2.MoveTo.Core.Translation
{
    public interface ITranslatorService
    {
        Task TranslateAsync(IDictionary<string, string> translations);
    }
}
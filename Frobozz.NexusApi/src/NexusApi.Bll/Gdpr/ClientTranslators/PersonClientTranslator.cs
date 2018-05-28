using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Crud.ClientTranslators;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ClientTranslators
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class PersonClientTranslator : CrudClientTranslator<PersonCreate, Person>, IPersonService
    {
        private readonly IGdprCapability _gdprCapability;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonClientTranslator(IGdprCapability gdprCapability, System.Func<string> getclientNameMethod, ITranslatorService translatorService)
        :base(gdprCapability.PersonService, "person.id", getclientNameMethod, translatorService)
        {
            _gdprCapability = gdprCapability;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var translator = CreateTranslator();
            var decoratedResult = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            await translator.Add(decoratedResult).ExecuteAsync();
            return translator.Translate(decoratedResult);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Crud.ServerTranslators.To;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.To
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class PersonServerTranslatorTo : CrudToServerTranslator<PersonCreate, Person>, IPersonService
    {
        private readonly IGdprCapability _gdprCapability;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonServerTranslatorTo(IGdprCapability gdprCapability, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        :base(gdprCapability.PersonService, "person.id",  getServerNameMethod, translatorService)
        {
            _gdprCapability = gdprCapability;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var translator = CreateTranslator();
            var result = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            return translator.DecorateItem(result);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.MoveTo.Core.ServerTranslators;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class PersonServerTranslator : CrudServerTranslator<Person>, IPersonService
    {
        private readonly IGdprCapability _gdprCapability;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonServerTranslator(IGdprCapability gdprCapability)
        :base(gdprCapability.PersonService, "person.id")
        {
            _gdprCapability = gdprCapability;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var translator = new Translator(ServerName);
            var result = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            return translator.DecorateItem(result);
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Crud.ServerTranslators.From;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class PersonServerTranslatorFrom : CrudServerTranslatorFrom<Person>, IPersonService
    {
        private readonly IGdprCapability _gdprCapability;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonServerTranslatorFrom(IGdprCapability gdprCapability, System.Func<string> getServerNameMethod)
        :base(gdprCapability.PersonService, "person.id", getServerNameMethod)
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

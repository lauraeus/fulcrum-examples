using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.MoveTo.Core.ClientTranslators;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.TranslateClient
{
    /// <summary>
    /// Client translator
    /// </summary>
    [RoutePrefix("api/Persons")]
    public class PersonClientClientTranslator : CrudClientTranslator<Person>, IPersonService
    {
        private readonly IGdprCapability _gdprCapability;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonClientClientTranslator(IGdprCapability gdprCapability)
        :base(gdprCapability.PersonService, "person.id")
        {
            _gdprCapability = gdprCapability;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var translator = new TranslationHelper(ClientName);
            var decoratedResult = await _gdprCapability.PersonService.FindFirstOrDefaultByNameAsync(name, token);
            await translator.Add(decoratedResult).ExecuteAsync();
            return translator.Translate(decoratedResult);
        }
    }
}

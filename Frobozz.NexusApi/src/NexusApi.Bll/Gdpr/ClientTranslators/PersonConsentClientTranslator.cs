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
    public class PersonConsentClientTranslator : SlaveToMasterClientTranslator<PersonConsentCreate, PersonConsent>, IPersonConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentClientTranslator(IGdprCapability gdprCapability, System.Func<string> getclientNameMethod, ITranslatorService translatorService)
        :base(gdprCapability.PersonConsentService, "person.id", "consent.id", getclientNameMethod, translatorService)
        {
        }
    }
}

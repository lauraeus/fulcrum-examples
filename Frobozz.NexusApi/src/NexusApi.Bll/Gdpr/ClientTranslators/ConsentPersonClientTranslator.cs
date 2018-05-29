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
    public class ConsentPersonClientTranslator : SlaveToMasterClientTranslator<PersonConsentCreate, PersonConsent>, IConsentPersonService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentPersonClientTranslator(IGdprCapability gdprCapability, System.Func<string> getclientNameMethod, ITranslatorService translatorService)
        :base(gdprCapability.PersonConsentService, "consent.id", "person.id", getclientNameMethod, translatorService)
        {
        }
    }
}

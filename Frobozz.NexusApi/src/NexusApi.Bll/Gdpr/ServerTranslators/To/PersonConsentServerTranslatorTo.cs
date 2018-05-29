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
    public class PersonConsentServerTranslatorTo : SlaveToMasterToServerTranslator<PersonConsentCreate, PersonConsent>, IPersonConsentService
    {

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentServerTranslatorTo(IGdprCapability gdprCapability, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        :base(gdprCapability.PersonConsentService, getServerNameMethod, translatorService)
        {
        }
    }
}

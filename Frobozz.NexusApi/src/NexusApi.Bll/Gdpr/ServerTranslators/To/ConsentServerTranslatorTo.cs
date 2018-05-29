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
    public class ConsentServerTranslatorTo : CrudToServerTranslator<ConsentCreate, Consent>, IConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentServerTranslatorTo(IGdprCapability gdprCapability, System.Func<string> getServerNameMethod, ITranslatorService translatorService)
        :base(gdprCapability.ConsentService, "consent.id", getServerNameMethod, translatorService)
        {
        }
    }
}

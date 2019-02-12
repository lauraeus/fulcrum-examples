using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.ClientTranslators;
using Nexus.Link.Libraries.Core.Translation;

namespace Frobozz.NexusApi.Bll.Gdpr.ClientTranslators
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class ConsentClientTranslator : CrudClientTranslator<ConsentCreate, Consent>, IConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentClientTranslator(IGdprCapability gdprCapability, System.Func<string> getclientNameMethod, ITranslatorService translatorService)
        :base(gdprCapability.ConsentService, "consent.id", getclientNameMethod, translatorService)
        {
        }
    }
}

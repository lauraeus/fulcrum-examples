using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Crud.ServerTranslators.From;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class ConsentServerTranslatorFrom : CrudFromServerTranslator<ConsentCreate, Consent>, IConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentServerTranslatorFrom(IGdprCapability gdprCapability, System.Func<string> getServerNameMethod)
        :base(gdprCapability.ConsentService, "consent.id", getServerNameMethod)
        {
        }
    }
}

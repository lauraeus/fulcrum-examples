using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.ServerTranslators.From;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class ConsentPersonServerTranslatorFrom : SlaveToMasterFromServerTranslator<PersonConsentCreate, PersonConsent>, IConsentPersonService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentPersonServerTranslatorFrom(IGdprCapability gdprCapability, System.Func<string> getServerNameMethod)
        :base(gdprCapability.PersonConsentService, "consent.id", "person.id", getServerNameMethod)
        {
        }
    }
}

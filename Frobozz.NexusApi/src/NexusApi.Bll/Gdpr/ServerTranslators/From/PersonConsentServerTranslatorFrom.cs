using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Crud.ServerTranslators.From;

namespace Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class PersonConsentServerTranslatorFrom : SlaveToMasterFromServerTranslator<PersonConsentCreate, PersonConsent>, IPersonConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentServerTranslatorFrom(IGdprCapability gdprCapability, System.Func<string> getServerNameMethod)
        :base(gdprCapability.PersonConsentService, "person.id", "consent.id", getServerNameMethod)
        {
        }
    }
}

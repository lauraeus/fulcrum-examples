using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Extensions.Caching.Distributed;
using Nexus.Link.Libraries.Crud.Cache;

namespace Frobozz.NexusApi.Bll.Gdpr.Caches
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class PersonConsentCache : SlaveToMasterAutoCache<PersonConsentCreate, PersonConsent, string>, IPersonConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentCache(IGdprCapability gdprCapability, IDistributedCache cache)
        :base(gdprCapability.PersonConsentService, cache)
        {
        }
    }
}

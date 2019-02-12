using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Extensions.Caching.Distributed;
using Nexus.Link.Libraries.Crud.Cache;

namespace Frobozz.NexusApi.Bll.Gdpr.Caches
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class ConsentCache : CrudAutoCache<ConsentCreate, Consent, string>, IConsentService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentCache(IGdprCapability gdprCapability, IDistributedCache cache)
        :base(gdprCapability.ConsentService, cache)
        {
        }
    }
}

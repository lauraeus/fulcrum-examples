using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Microsoft.Extensions.Caching.Distributed;
using Nexus.Link.Libraries.Crud.Cache;

namespace Frobozz.NexusApi.Bll.Gdpr.Caches
{
    /// <summary>
    /// Client translator
    /// </summary>
    public class ConsentPersonCache : SlaveToMasterAutoCache<PersonConsentCreate, PersonConsent, string>, IConsentPersonService
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentPersonCache(IGdprCapability gdprCapability, IDistributedCache cache)
        :base(gdprCapability.PersonConsentService, cache)
        {
        }
    }
}

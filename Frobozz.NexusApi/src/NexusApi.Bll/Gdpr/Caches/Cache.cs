using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Cache;

namespace Frobozz.NexusApi.Bll.Gdpr.Caches
{
    /// <inheritdoc />
    public class Cache : IGdprCapability
    {
        /// <inheritdoc />
        public Cache(IGdprCapability capablity)
        {
            // TODO: Change the async calls into AsyncLazy
            var metaCache = new CrudMemory<DistributedCacheMemory, string>();
            var cacheFactory = new DistributedCacheFactoryMemory(metaCache);
            var personCache = cacheFactory.GetOrCreateDistributedCacheAsync("Person").Result;
            PersonService = new PersonCache(capablity, personCache);
            var consentCache = cacheFactory.GetOrCreateDistributedCacheAsync("Consent").Result;
            ConsentService = new CrudAutoCache<Consent, string>(capablity.ConsentService, consentCache);
            var personConsentCache = cacheFactory.GetOrCreateDistributedCacheAsync("PersonConsent").Result;
            PersonConsentService = new ManyToOneAutoCache<PersonConsent, string>(capablity.PersonConsentService, personConsentCache);
        }

        /// <inheritdoc />
        public IPersonService PersonService { get; }

        /// <inheritdoc />
        public ICrud<Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public IManyToOneRelation<PersonConsent, string> PersonConsentService { get; }
    }
}
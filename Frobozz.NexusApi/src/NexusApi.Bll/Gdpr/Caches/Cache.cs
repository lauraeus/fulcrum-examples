using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Nexus.Link.Libraries.Core.Assert;
using Nexus.Link.Libraries.Crud.Cache;
using Nexus.Link.Libraries.Crud.Interfaces;
using Nexus.Link.Libraries.Crud.MemoryStorage;

namespace Frobozz.NexusApi.Bll.Gdpr.Caches
{
    /// <inheritdoc />
    public class Cache : IGdprCapability
    {
        private readonly IGdprCapability _capablity;
        private IPersonService _personService;
        private readonly object _lock = new object();

        /// <inheritdoc />
        public Cache(IGdprCapability capablity)
        {
            _capablity = capablity;
            // TODO: Change the async calls into AsyncLazy
            var metaCache = new CrudMemory<DistributedCacheMemory, string>();
            var cacheFactory = new DistributedCacheFactoryMemory(metaCache);
            var personCache = cacheFactory.GetOrCreateDistributedCacheAsync("Person").Result;
            _personService = new PersonCache(capablity, personCache);
            var consentCache = cacheFactory.GetOrCreateDistributedCacheAsync("Consent").Result;
            // ReSharper disable once SuspiciousTypeConversion.Global
            ConsentService = new ConsentCache(capablity, consentCache);
            var personConsentCache = cacheFactory.GetOrCreateDistributedCacheAsync("PersonConsent").Result;
            // ReSharper disable once SuspiciousTypeConversion.Global
            PersonConsentService =
                new PersonConsentCache(capablity, personConsentCache);
            var consentPersonCache = cacheFactory.GetOrCreateDistributedCacheAsync("ConsentPerson").Result;
            // ReSharper disable once SuspiciousTypeConversion.Global
            ConsentPersonService =
                new ConsentPersonCache(capablity, consentPersonCache);
        }

        /// <inheritdoc />
        public IPersonService PersonService
        {
            // TODO: Use this pattern for the other services.
            get
            {
                if (_personService != null) return _personService;
                lock (_lock)
                {
                    _personService = _personService ?? new PersonCache(_capablity, null);
                    return _personService;
                }
            }
        }

        /// <inheritdoc />
        public IConsentService ConsentService { get; }

        /// <inheritdoc />
        public IPersonConsentService PersonConsentService { get; }

        /// <inheritdoc />
        public IConsentPersonService ConsentPersonService { get; }
    }
}
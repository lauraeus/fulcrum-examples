using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Crud.Cache;
using Xlent.Lever.Libraries2.Core.Crud.Interfaces;
using Xlent.Lever.Libraries2.Core.Crud.MemoryStorage;

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
            ConsentService = new CrudAutoCache<ConsentCreate, Consent, string>(capablity.ConsentService, consentCache);
            FulcrumAssert.IsNotNull(ConsentService);
            var personConsentCache = cacheFactory.GetOrCreateDistributedCacheAsync("PersonConsent").Result;
            // ReSharper disable once SuspiciousTypeConversion.Global
            PersonConsentService =
                new SlaveToMasterAutoCache<PersonConsentCreate, PersonConsent, string>(capablity.PersonConsentService, personConsentCache);
            FulcrumAssert.IsNotNull(PersonConsentService);
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
        public ICrud<ConsentCreate, Consent, string> ConsentService { get; }

        /// <inheritdoc />
        public ISlaveToMaster<PersonConsentCreate, PersonConsent, string> PersonConsentService { get; }
    }
}
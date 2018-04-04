using System;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class ConsentLogic : CrudMapper<Consent, string, IStorage, ConsentTable, Guid>, IConsentService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public ConsentLogic(IStorage storage)
        :base(storage.Consent, storage)
        {
            _storage = storage;
        }
    }
}

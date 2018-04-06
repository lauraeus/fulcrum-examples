using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Frobozz.GdprConsent.NexusFacade.WebApi.Mappers;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class PersonConsentLogic : ManyToOneMapper<PersonConsent, string, IStorage, PersonConsentTable, Guid>, IPersonConsentService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentLogic(IStorage storage)
        :base(storage, storage.PersonConsent, new PersonConsentMapper())
        {
            _storage = storage;
        }
    }
}

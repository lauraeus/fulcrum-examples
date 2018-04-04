using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Model;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    internal class PersonConsentLogic : IPersonConsentService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentLogic(IStorage storage)
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}/Consents")]
        public async Task<IEnumerable<PersonConsent>> ReadChildrenAsync(string parentId, int limit = int.MaxValue)
        {
            var id = new Guid(parentId);
            var personConsents = await _storage.PersonConsent.ReadByReference1Async(id, limit);
            var consentsDb = await _storage.PersonConsent.ReadReferencedItemsByReference1Async(id, limit);
            var consents = new List<PersonConsent>();
            var personConsentDbArray = personConsents as PersonConsentTable[] ?? personConsents.ToArray();
            foreach (var consentDb in consentsDb)
            {
                var personConsent = personConsentDbArray.FirstOrDefault(pc => pc.ConsentId == consentDb.Id);
                if (personConsent == null) continue;
                var consent = ToService(consentDb, personConsent);
                consents.Add(consent);
                ;
            }

            return consents;
        }

        /// <inheritdoc />
        public Task DeleteChildrenAsync(string parentId)
        {
            throw new NotImplementedException();
        }

        private PersonConsent ToService(ConsentTable source, PersonConsentTable personConsent, bool nullIsOk = false)
        {
            if (nullIsOk && source == null) return null;
            InternalContract.RequireNotNull(source, nameof(source));
            InternalContract.RequireValidated(source, nameof(source));
            var target = new PersonConsent
            {
                Name = source.Name,
                HasGivenConsent = personConsent.HasGivenConsent,
                Id = source.Id.ToString()
            };
            FulcrumAssert.IsValidated(target);
            return target;
        }
    }
}

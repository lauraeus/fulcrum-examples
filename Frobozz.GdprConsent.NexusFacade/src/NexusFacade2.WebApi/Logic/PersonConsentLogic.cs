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

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class PersonConsentLogic : IPersonConsentService
    {
        private readonly IStorage _storage;

        protected PersonConsentConsentMapper Mapper { get; }

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonConsentLogic(IStorage storage)
        {
            _storage = storage;
            Mapper = new PersonConsentConsentMapper();
        }

        /// <inheritdoc />
        public Task<PageEnvelope<PersonConsent>> ReadChildrenWithPagingAsync(string parentId, int offset, int? limit = null, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [HttpGet]
        [Route("{id}/Consents")]
        public async Task<IEnumerable<PersonConsent>> ReadChildrenAsync(string parentId, int limit = int.MaxValue, CancellationToken token = default(CancellationToken))
        {
            var id = new Guid(parentId);
            var personConsentsDb = await _storage.PersonConsent.ReadByReference1Async(id, limit, token);
            var personConsentDbArray = personConsentsDb as PersonConsentTable[] ?? personConsentsDb.ToArray();
            var personConsentTasks =
                personConsentDbArray.Select(async pc => await Mapper.CreateAndMapFromServerAsync(pc, _storage, token));
            return await Task.WhenAll(personConsentTasks);
        }

        /// <inheritdoc />
        public Task DeleteChildrenAsync(string parentId, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}

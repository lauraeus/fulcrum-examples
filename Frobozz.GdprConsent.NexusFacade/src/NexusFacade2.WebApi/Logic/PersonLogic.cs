using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Frobozz.GdprConsent.NexusFacade.WebApi.Mappers;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusFacade.WebApi.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class PersonLogic : CrudMapper<Person, string, IStorage, PersonTable, Guid>, IPersonService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonLogic(IStorage storage)
        :base(storage, storage.Person, new PersonMapper())
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var enumerator = new PageEnvelopeEnumeratorAsync<PersonTable>((o, t) => _storage.Person.ReadAllWithPagingAsync(o, null, t), token);
            while (await enumerator.MoveNextAsync())
            {
                if (enumerator.Current.Name == name) return await Mapper.CreateAndMapFromServerAsync(enumerator.Current, _storage, token);
            }
            return null;
        }
    }
}

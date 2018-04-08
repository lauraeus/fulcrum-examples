using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Gdpr.Mappers
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class PersonMapper : CrudMapper<Person, string, IStorage, PersonTable, Guid>, IPersonService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonMapper(IStorage storage)
        :base(storage, storage.Person, new PersonModelMapper())
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var enumerator = new PageEnvelopeEnumeratorAsync<PersonTable>((o, t) => _storage.Person.ReadAllWithPagingAsync(o, null, t), token);
            while (await enumerator.MoveNextAsync())
            {
                if (enumerator.Current.Name == name) return await ModelMapper.CreateAndMapFromServerAsync(enumerator.Current, _storage, token);
            }
            return null;
        }
    }
}

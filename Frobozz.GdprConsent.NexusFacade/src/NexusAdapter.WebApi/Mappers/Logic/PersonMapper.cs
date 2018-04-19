using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model;
using Xlent.Lever.Libraries2.Core.Crud.Mappers;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class PersonMapper : CrudMapper<Person, string, PersonTable, Guid>, IPersonService
    {
        private readonly IStorage _storage;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonMapper(IStorage storage)
        :base(storage.Person, new PersonModelMapper(storage))
        {
            _storage = storage;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var enumerator = new PageEnvelopeEnumeratorAsync<PersonTable>((o, t) => _storage.Person.ReadAllWithPagingAsync(o, null, t), token);
            while (await enumerator.MoveNextAsync())
            {
                if (enumerator.Current.Name == name) return await CrudModelMapper.MapFromServerAsync(enumerator.Current, token);
            }
            return null;
        }
    }
}

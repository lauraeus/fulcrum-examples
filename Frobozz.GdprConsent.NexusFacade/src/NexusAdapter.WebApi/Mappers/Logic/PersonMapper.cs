using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Model;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
using Xlent.Lever.Libraries2.MoveTo.Core.Mapping;

namespace Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic
{
    /// <summary>
    /// Logic for Product. 
    /// </summary>
    public class PersonMapper : CrudMapper<Person, string, IServerLogic, PersonTable, Guid>, IPersonService
    {
        private readonly IServerLogic _serverLogic;

        /// <summary>
        /// Constructor 
        /// </summary>
        public PersonMapper(IServerLogic serverLogic)
        :base(serverLogic, serverLogic.Person, new PersonModelMapper())
        {
            _serverLogic = serverLogic;
        }

        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            var enumerator = new PageEnvelopeEnumeratorAsync<PersonTable>((o, t) => _serverLogic.Person.ReadAllWithPagingAsync(o, null, t), token);
            while (await enumerator.MoveNextAsync())
            {
                if (enumerator.Current.Name == name) return await ModelMapper.CreateAndMapFromServerAsync(enumerator.Current, _serverLogic, token);
            }
            return null;
        }
    }
}

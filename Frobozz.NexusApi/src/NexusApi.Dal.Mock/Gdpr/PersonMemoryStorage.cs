using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Xlent.Lever.Libraries2.Core.Assert;
using Xlent.Lever.Libraries2.Crud.MemoryStorage;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IPersonService" />
    public class PersonMemoryStorage : CrudMemory<PersonCreate, Person, string>, IPersonService
    {
        /// <inheritdoc />
        public async Task<Person> FindFirstOrDefaultByNameAsync(string name, CancellationToken token = default(CancellationToken))
        {
            InternalContract.RequireNotNullOrWhitespace(name, nameof(name));
            var enumerator =
                new PageEnvelopeEnumeratorAsync<Person>((offset, t) => ReadAllWithPagingAsync(offset, null, t), token);
            while (await enumerator.MoveNextAsync())
            {
                if (enumerator.Current.Name == name) return enumerator.Current;
            }

            return null;
        }
    }
}
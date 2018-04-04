using System;
using System.Linq;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Xlent.Lever.Libraries2.Core.Storage.Logic;

namespace Frobozz.NexusApi.Dal.Mock.Gdpr
{
    /// <inheritdoc cref="IPersonService" />
    public class PersonMemoryStorage : MemoryPersistance<Person, string>, IPersonService
    {
        /// <inheritdoc />
        public async Task<Person> GetRandomAsync()
        {
            var items = (await ReadAllAsync(100)).ToArray();
            var random = DateTimeOffset.Now.ToUnixTimeMilliseconds() % items.Length;
            return items[random];
        }
    }
}
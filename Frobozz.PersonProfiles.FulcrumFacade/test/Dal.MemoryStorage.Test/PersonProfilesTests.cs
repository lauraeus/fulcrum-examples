using System;
using System.Threading.Tasks;
using Frobozz.PersonProfiles.Dal.MemoryStorage.PersonProfile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Storage.Logic;
using Xlent.Lever.Libraries2.Standard.Storage.Model;
using Xlent.Lever.Libraries2.Standard.Storage.Test;

namespace Frobozz.PersonProfiles.Dal.MemoryStorage.Test
{
    [TestClass]
    public class MemoryStorageTest
    {
        private MemoryStorage<StorablePersonProfile> _storage;
        private StorageTestCrud<MemoryStorage<StorablePersonProfile>, StorablePersonProfile, Guid> _testCrud;

        [TestInitialize]
        public void Inititalize()
        {
            var methodName = $"{typeof(MemoryStorageTest).FullName}.{System.Reflection.MethodBase.GetCurrentMethod().Name}";
            _storage = new MemoryStorage<StorablePersonProfile>();
            // ReSharper disable once SuspiciousTypeConversion.Global
            var icrudStorage = _storage as ICrud<IStorableItem<Guid>, Guid>;
            FulcrumAssert.IsNotNull(icrudStorage, $"{methodName}: 6E1C95FD-6245-442F-9D76-FC34E9394672");
            _testCrud = new StorageTestCrud<MemoryStorage<StorablePersonProfile>, StorablePersonProfile, Guid>(_storage);
        }

        [TestMethod]
        public async Task TestCrud()
        {
            await _testCrud.RunAllTests();
        }

    }
}

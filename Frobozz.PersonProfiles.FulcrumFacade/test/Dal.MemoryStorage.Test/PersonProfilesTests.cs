using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xlent.Lever.Libraries2.Standard.Storage.Logic;
using Xlent.Lever.Libraries2.Standard.Storage.Test;

namespace Frobozz.PersonProfiles.Dal.MemoryStorage.Test
{
    [TestClass]
    public class MemoryStorageTest
    {
        private MemoryStorage<PersonProfile.PersonProfile> _storage;
        private StorageTestCrud<MemoryStorage<PersonProfile.PersonProfile>, PersonProfile.PersonProfile, Guid> _testCrud;

        [TestInitialize]
        public void Inititalize()
        {
            _storage = new MemoryStorage<PersonProfile.PersonProfile>();
            _testCrud = new StorageTestCrud<MemoryStorage<PersonProfile.PersonProfile>, PersonProfile.PersonProfile, Guid>(_storage);
        }

        [TestMethod]
        public async Task TestCrud()
        {
            await _testCrud.RunAllTests();
        }

    }
}

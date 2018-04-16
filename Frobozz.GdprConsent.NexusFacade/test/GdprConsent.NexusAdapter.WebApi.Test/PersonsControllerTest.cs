using System;
using System.Linq;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Controllers;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.SqlServer;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xlent.Lever.Libraries2.Core.Application;

namespace GdprConsent.NexusAdapter.WebApi.Test
{
    [TestClass]
    public class PersonsControllerTest
    {
        private IStorage _storage;
        private Guid _kalleAnka;
        private PersonsController _personsController;

        [TestInitialize]
        public async Task Initialize()
        {
            FulcrumApplicationHelper.UnitTestSetup(typeof(PersonsControllerTest).FullName);
            _storage = new SqlServerStorage("Data Source=WIN-7B74C50VA4D;Initial Catalog=LeverExampleGdpr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //_storage = new MemoryStorage();
            var gdprCapability = new Mapper(_storage);
            await DeleteAll();
            _kalleAnka = await CreateKalleAnkaAsync();
            _personsController = new PersonsController(gdprCapability);
        }

        private async Task DeleteAll()
        {
            await _storage.PersonConsent.DeleteAllAsync();
            await _storage.Address.DeleteAllAsync();
            await _storage.Consent.DeleteAllAsync();
            await _storage.Person.DeleteAllAsync();
        }

        [TestMethod]
        public async Task ReadPerson()
        {
            var person = await _personsController.ReadAsync(_kalleAnka.ToString());
            VerifyKalleAnka(person);
        }

        private void VerifyKalleAnka(Person person)
        {
            Assert.IsNotNull(person);
            Assert.AreEqual("Kalle Anka", person.Name);
            Assert.IsNotNull(person.Addresses);
            var addresses = person.Addresses.ToArray();
            Assert.AreEqual(2, addresses.Length);
            var address = addresses.FirstOrDefault(a => a.Type == "Public");
            Assert.IsNotNull(address);
            Assert.AreEqual("Kalles gata", address.Street);
            Assert.AreEqual("Ankeborg", address.City);
            address = addresses.FirstOrDefault(a => a.Type == "Postal");
            Assert.IsNotNull(address);
            Assert.AreEqual("Box 123", address.Street);
            Assert.AreEqual("Ankeborg", address.City);
        }

        private async Task<Guid> CreateKalleAnkaAsync()
        {
            var person = new PersonTable
            {
                Name = "Kalle Anka"
            };
            var personId = await _storage.Person.CreateAsync(person);
            var address = new AddressTable
            {
                Type = 1,
                Street = "Kalles gata",
                City = "Ankeborg",
                PersonId = personId
            };
            await _storage.Address.CreateAsync(address);
            address = new AddressTable
            {
                Type = 4,
                Street = "Box 123",
                City = "Ankeborg",
                PersonId = personId
            };
            await _storage.Address.CreateAsync(address);

            return personId;
        }
    }
}

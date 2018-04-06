using System;
using System.Linq;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.GdprConsent.NexusFacade.WebApi.Controllers;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal;
using Frobozz.GdprConsent.NexusFacade.WebApi.Dal.Model;
using Frobozz.GdprConsent.NexusFacade.WebApi.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xlent.Lever.Libraries2.Core.Application;
using Xlent.Lever.Libraries2.Core.Storage.Logic;
namespace GdprConsent.NexusFacade.WebApi.Test
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
            var personStorage = new MemoryPersistance<PersonTable, Guid>();
            var consentStorage = new MemoryPersistance<ConsentTable, Guid>();
            var addressStorage = new MemoryManyToOnePersistance<AddressTable, Guid>(a => a.PersonId);
            var personConsentStorage = new MemoryManyToOnePersistance<PersonConsentTable, Guid>(pc=>  pc.PersonId);
            _storage = new Storage(personStorage, addressStorage, consentStorage, personConsentStorage);
            _kalleAnka = await CreateKalleAnkaAsync();

            var personLogic = new PersonLogic(_storage);
            var consentLogic = new ConsentLogic(_storage);
            var personConsentLogic = new PersonConsentLogic(_storage);

            var gdprCapability = new GdprCapability(personLogic, consentLogic, personConsentLogic);

            _personsController = new PersonsController(gdprCapability);
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

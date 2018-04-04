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
    public class PersonsContentsControllerTest
    {
        private IStorage _storage;
        private Guid _kalleAnka;
        private PersonConsentsController _personConsentsController;
        private Guid _profilingConsentId;
        private Guid _marketingConsentId;

        [TestInitialize]
        public async Task Initialize()
        {
            FulcrumApplicationHelper.UnitTestSetup(typeof(PersonsControllerTest).FullName);
            var personStorage = new MemoryPersistance<PersonTable, Guid>();
            var consentStorage = new MemoryPersistance<ConsentTable, Guid>();
            var addressStorage = new MemoryManyToOnePersistance<AddressTable, Guid>(a => a.PersonId);
            var personConsentStorage = new MemoryManyToManyPersistance<PersonConsentTable, PersonTable, ConsentTable, Guid>(pc=>  pc.PersonId, pc=> pc.ConsentId, personStorage, consentStorage);
            _storage = new Storage(personStorage, addressStorage, consentStorage, personConsentStorage);

            var personLogic = new PersonLogic(_storage);
            var consentLogic = new ConsentLogic(_storage);
            var personConsentLogic = new PersonConsentLogic(_storage);
            var gdprCapability = new GdprCapability(personLogic, consentLogic, personConsentLogic);
            _personConsentsController = new PersonConsentsController(gdprCapability);

            await CreateConsents();
            _kalleAnka = await CreateKalleAnkaAsync();
        }

        [TestMethod]
        public async Task ReadConsentsForAPerson()
        {
            var personConsents = (await _personConsentsController.ReadChildrenAsync(_kalleAnka.ToString())).ToArray();
            VerifyConsents(personConsents, true, false);
        }

        private void VerifyConsents(PersonConsent[] personConsents, bool profiling, bool marketing)
        {
            Assert.IsNotNull(personConsents);
            Assert.AreEqual(2, personConsents.Length);
            var personConsent = personConsents.FirstOrDefault(a => a.Name == "Profiling");
            Assert.IsNotNull(personConsent);
            Assert.AreEqual(profiling,  personConsent.HasGivenConsent);
            personConsent = personConsents.FirstOrDefault(a => a.Name == "Marketing");
            Assert.IsNotNull(personConsent);
            Assert.AreEqual(marketing, personConsent.HasGivenConsent);
        }

        private async Task CreateConsents()
        {
            var consent = new ConsentTable
            {
                Name = "Profiling"
            };
            _profilingConsentId = await _storage.Consent.CreateAsync(consent);

            consent = new ConsentTable
            {
                Name = "Marketing"
            };
            _marketingConsentId = await _storage.Consent.CreateAsync(consent);
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

            await CreatePersonConsentsAsync(personId, true, false);

            return personId;
        }

        private async Task CreatePersonConsentsAsync(Guid personId, bool profiling, bool marketing)
        {
            var personConsent = new PersonConsentTable
            {
                ConsentId = _profilingConsentId,
                PersonId = personId,
                HasGivenConsent = profiling
            };
            await _storage.PersonConsent.CreateAsync(personConsent);
            personConsent = new PersonConsentTable
            {
                ConsentId = _marketingConsentId,
                PersonId = personId,
                HasGivenConsent = marketing
            };
            await _storage.PersonConsent.CreateAsync(personConsent);
        }
    }
}

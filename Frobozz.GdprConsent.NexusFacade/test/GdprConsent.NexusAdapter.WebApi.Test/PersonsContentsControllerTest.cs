using System;
using System.Linq;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Controllers;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.SqlServer;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nexus.Link.Libraries.Core.Application;

namespace GdprConsent.NexusAdapter.WebApi.Test
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
            _storage = new SqlServerStorage("Data Source=WIN-7B74C50VA4D;Initial Catalog=LeverExampleGdpr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //_storage = new MemoryStorage();
            var gdprCapability = new Mapper(_storage);
            _personConsentsController = new PersonConsentsController(gdprCapability);

            await CreateConsents();
            _kalleAnka = await CreateKalleAnkaAsync();
        }

        [TestMethod]
        public async Task ReadConsentsForAPerson()
        {
            var page = await _personConsentsController.ReadChildrenWithPagingAsync(_kalleAnka.ToString(), 0);
            Assert.IsNotNull(page?.Data);
            var personConsents = page.Data.ToArray();
            VerifyConsents(personConsents, true, false);
        }

        private void VerifyConsents(PersonConsent[] personConsents, bool profiling, bool marketing)
        {
            Assert.IsNotNull(personConsents);
            Assert.AreEqual(2, personConsents.Length);
            var personConsent = personConsents.FirstOrDefault(a => a.ConsentName == "Profiling");
            Assert.IsNotNull(personConsent);
            Assert.AreEqual(profiling,  personConsent.HasGivenConsent);
            personConsent = personConsents.FirstOrDefault(a => a.ConsentName == "Marketing");
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

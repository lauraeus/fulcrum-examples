using System;
using System.Linq;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Contracts;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Controllers;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.Mock;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Dal.SqlServer;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers;
using Frobozz.GdprConsent.NexusAdapter.WebApi.Mappers.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xlent.Lever.Libraries2.Core.Application;

namespace GdprConsent.NexusAdapter.WebApi.Test
{
    [TestClass]
    public class PersonsContentsControllerTest
    {
        private IServerLogic _serverLogic;
        private Guid _kalleAnka;
        private PersonConsentsController _personConsentsController;
        private Guid _profilingConsentId;
        private Guid _marketingConsentId;

        [TestInitialize]
        public async Task Initialize()
        {
            FulcrumApplicationHelper.UnitTestSetup(typeof(PersonsControllerTest).FullName);
            _serverLogic = new SqlServerStorage("Data Source=WIN-7B74C50VA4D;Initial Catalog=LeverExampleGdpr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //_storage = new MemoryStorage();
            var gdprCapability = new Mapper(_serverLogic);
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
            _profilingConsentId = await _serverLogic.Consent.CreateAsync(consent);

            consent = new ConsentTable
            {
                Name = "Marketing"
            };
            _marketingConsentId = await _serverLogic.Consent.CreateAsync(consent);
        }

        private async Task<Guid> CreateKalleAnkaAsync()
        {
            var person = new PersonTable
            {
                Name = "Kalle Anka"
            };
            var personId = await _serverLogic.Person.CreateAsync(person);
            var address = new AddressTable
            {
                Type = 1,
                Street = "Kalles gata",
                City = "Ankeborg",
                PersonId = personId
            };
            await _serverLogic.Address.CreateAsync(address);
            address = new AddressTable
            {
                Type = 4,
                Street = "Box 123",
                City = "Ankeborg",
                PersonId = personId
            };
            await _serverLogic.Address.CreateAsync(address);

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
            await _serverLogic.PersonConsent.CreateAsync(personConsent);
            personConsent = new PersonConsentTable
            {
                ConsentId = _marketingConsentId,
                PersonId = personId,
                HasGivenConsent = marketing
            };
            await _serverLogic.PersonConsent.CreateAsync(personConsent);
        }
    }
}

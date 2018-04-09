using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.NexusApi.Bll.Gdpr.ServerTranslators;
using Frobozz.NexusApi.Bll.Test.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Test.Gdpr.ServerTranslators
{
    [TestClass]
    public class PersonServerTranslatorTest
    {
        private Mock<IGdprCapability> _serviceMock;
        private PersonServerTranslator _serverTranslator;
        private Person _clientPerson;
        private TranslatorServiceMock _translationServiceMock;
        private Person _serverPerson;

        [TestInitialize]
        public void Initialize()
        {
            _translationServiceMock = new TranslatorServiceMock();
            _serviceMock = new Mock<IGdprCapability>();
            _serverTranslator = new PersonServerTranslator(_serviceMock.Object, _translationServiceMock);
            var name = "Kalle Anka";
            var addresses = new Address[] { };
            var etag = Guid.NewGuid().ToString();
            _clientPerson = new Person
            {
                Id = "client-1",
                Name = name,
                Addresses = addresses,
                Etag = etag
            };
            _serverPerson = new Person
            {
                Id = "server-1",
                Name = name,
                Addresses = addresses,
                Etag = etag
            };
        }
        [TestMethod]
        public async Task FindFirstOrDefaultByNameAsync()
        {
            var name = _clientPerson.Name;
            var id = _serverPerson.Id;
            _serviceMock.Setup(capability =>
                capability.PersonService.FindFirstOrDefaultByNameAsync(It.Is<string>(s => s == name),
                    CancellationToken.None)).ReturnsAsync(_serverPerson);
            var person = await _serverTranslator.FindFirstOrDefaultByNameAsync(_clientPerson.Name);
            var translator = new Translator("server.name", _translationServiceMock);
            Assert.AreEqual(translator.Decorate("person.id", id), person.Id);
            _serviceMock.Verify();
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.NexusApi.Bll.Gdpr.ClientTranslators;
using Frobozz.NexusApi.Bll.Test.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Test.Gdpr.ClientTranslators
{
    [TestClass]
    public class PersonClientTranslatorTest
    {
        private Mock<IGdprCapability> _serviceMock;
        private PersonClientTranslator _clientTranslator;
        private Person _serverPerson;
        private Person _clientPerson;
        private TranslatorServiceMock _translationServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _translationServiceMock = new TranslatorServiceMock();
            _serviceMock = new Mock<IGdprCapability>();
            _clientTranslator = new PersonClientTranslator(_serviceMock.Object, _translationServiceMock);
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
            var id = _clientPerson.Id;
            _serviceMock.Setup(capability =>
                capability.PersonService.FindFirstOrDefaultByNameAsync(It.Is<string>(s => s == name),
                    CancellationToken.None)).ReturnsAsync(_clientPerson);
            var person = await _clientTranslator.FindFirstOrDefaultByNameAsync(_clientPerson.Name);
            var translator = new Translator("server.name", _translationServiceMock);
            Assert.AreEqual(id, person.Id);
            _serviceMock.Verify();
        }
    }
}

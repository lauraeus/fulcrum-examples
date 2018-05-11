using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.Contracts.GdprCapability.Interfaces;
using Frobozz.Contracts.GdprCapability.Model;
using Frobozz.NexusApi.Bll.Gdpr.ClientTranslators;
using Frobozz.NexusApi.Bll.Test.Support;
using Frobozz.NexusApi.Dal.Mock.Translator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.NexusApi.Bll.Test.Gdpr.ClientTranslators
{
    [TestClass]
    public class PersonClientTranslatorTest
    {
        private Mock<IGdprCapability> _serviceMock;
        private PersonClientTranslator _clientTranslator;
        private Person _serverPerson;
        private Person _clientPerson;
        private TranslatorServiceMock _translationServiceFromServerMock;

        [TestInitialize]
        public void Initialize()
        {
            _translationServiceFromServerMock = new TranslatorServiceMock(MockTestContext.GetClientName(), true);
            _serviceMock = new Mock<IGdprCapability>();
            _clientTranslator = new PersonClientTranslator(_serviceMock.Object, MockTestContext.GetClientName, _translationServiceFromServerMock);
            var name = "Kalle Anka";
            var addresses = new Address[] { };
            var etag = Guid.NewGuid().ToString();
            _clientPerson = new Person
            {
                Id = $"{MockTestContext.GetClientName()}-1",
                Name = name,
                Addresses = addresses,
                Etag = etag
            };
            _serverPerson = new Person
            {
                Id = $"1",
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
            var translator = new Translator(MockTestContext.GetClientName(), _translationServiceFromServerMock);
            Assert.AreEqual(id, person.Id);
            _serviceMock.Verify();
        }
    }
}

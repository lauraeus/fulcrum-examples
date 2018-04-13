using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr.Logic;
using Frobozz.CapabilityContracts.Gdpr.Model;
using Frobozz.NexusApi.Bll.Gdpr.ServerTranslators.From;
using Frobozz.NexusApi.Bll.Test.Support;
using Frobozz.NexusApi.Dal.Mock.Translator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Core.Translation;

namespace Frobozz.NexusApi.Bll.Test.Gdpr.ServerTranslatorsFrom
{
    [TestClass]
    public class PersonServerTranslatorFromTest
    {
        private Mock<IGdprCapability> _serviceMock;
        private PersonServerTranslatorFrom _serverTranslator;
        private Person _clientPerson;
        private TranslatorServiceMock _translationServiceMock;
        private Person _serverPerson;

        [TestInitialize]
        public void Initialize()
        {
            _translationServiceMock = new TranslatorServiceMock(MockTestContext.GetClientName(), true);
            _serviceMock = new Mock<IGdprCapability>();
            _serverTranslator = new PersonServerTranslatorFrom(_serviceMock.Object, MockTestContext.GetServerName);
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
            var id = _serverPerson.Id;
            _serviceMock.Setup(capability =>
                capability.PersonService.FindFirstOrDefaultByNameAsync(It.Is<string>(s => s == name),
                    CancellationToken.None)).ReturnsAsync(_serverPerson);
            var person = await _serverTranslator.FindFirstOrDefaultByNameAsync(_clientPerson.Name);
            var translator = new Translator(MockTestContext.GetServerName(), _translationServiceMock);
            Assert.AreEqual(translator.Decorate("person.id", id), person.Id);
            _serviceMock.Verify();
        }
    }
}

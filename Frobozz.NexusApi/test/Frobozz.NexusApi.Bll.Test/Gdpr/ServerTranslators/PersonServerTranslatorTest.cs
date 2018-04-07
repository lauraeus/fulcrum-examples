using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.NexusApi.Bll.Gdpr.ServerTranslators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Test.Gdpr.ServerTranslators
{
    [TestClass]
    public class PersonServerTranslatorTest
    {
        private Mock<IGdprCapability> _mock;
        private PersonServerTranslator _serverTranslator;
        private Person _person;

        [TestInitialize]
        public void Initialize()
        {
            _mock = new Mock<IGdprCapability>();
            _serverTranslator = new PersonServerTranslator(_mock.Object);
            _person = new Person
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Kalle Anka",
                Addresses = new Address[] {},
                Etag = Guid.NewGuid().ToString()
            };
        }
        [TestMethod]
        public async Task FindFirstOrDefaultByNameAsync()
        {
            var name = _person.Name;
            var id = _person.Id;
            _mock.Setup(capability =>
                capability.PersonService.FindFirstOrDefaultByNameAsync(It.Is<string>(s => s == name),
                    CancellationToken.None)).ReturnsAsync(_person);
            var person = await _serverTranslator.FindFirstOrDefaultByNameAsync(_person.Name);
            var translator = new Translator("server.name");
            Assert.AreEqual(translator.Decorate("person.id", id), person.Id);
            _mock.Verify();
        }
    }
}

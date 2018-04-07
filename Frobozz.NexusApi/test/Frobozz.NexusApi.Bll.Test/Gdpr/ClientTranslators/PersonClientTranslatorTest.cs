using System;
using System.Threading;
using System.Threading.Tasks;
using Frobozz.CapabilityContracts.Gdpr;
using Frobozz.NexusApi.Bll.Gdpr.ClientTranslators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.MoveTo.Core.Translation;

namespace Frobozz.NexusApi.Bll.Test.Gdpr.ClientTranslators
{
    [TestClass]
    public class PersonClientTranslatorTest
    {
        private Mock<IGdprCapability> _mock;
        private PersonClientTranslator _clientTranslator;
        private Person _person;

        [TestInitialize]
        public void Initialize()
        {
            _mock = new Mock<IGdprCapability>();
            _clientTranslator = new PersonClientTranslator(_mock.Object);
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
            _mock.Setup(capability =>
                capability.PersonService.FindFirstOrDefaultByNameAsync(It.Is<string>(s => s == name),
                    CancellationToken.None)).ReturnsAsync(_person);
            var person = await _clientTranslator.FindFirstOrDefaultByNameAsync("Name");
            var translator = new Translator("server.name");
            Assert.AreEqual(translator.Decorate("person.id", _person.Id), person.Id);
            _mock.Verify();
        }
    }
}

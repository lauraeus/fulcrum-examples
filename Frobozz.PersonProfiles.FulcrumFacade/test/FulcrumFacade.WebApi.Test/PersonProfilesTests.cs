using System;
using System.Threading.Tasks;
using Frobozz.PersonProfiles.Bll;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Frobozz.PersonProfiles.FulcrumFacade.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi.Tests
{
    [TestClass]
    public class PersonProfilesTests
    {
        private static readonly string Namespace = typeof(PersonProfilesTests).Namespace;
        private PersonProfilesController _controller;
        private Mock<IPersonProfilesFunctionality> _PersonProfilesFunctionality;
        private PersonProfile _bllPerson;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
           
            _PersonProfilesFunctionality = new Mock<IPersonProfilesFunctionality>();
            _controller = new PersonProfilesController(_PersonProfilesFunctionality.Object);
            _bllPerson = new PersonProfile
            {
                Id = Guid.NewGuid().ToString(),
                ETag = Guid.NewGuid().ToString(),
                GivenName = "Joe",
                Surname = "Smith"
            };
            FulcrumAssert.IsValidated(_bllPerson, $"{Namespace}: C3A453C9-77AA-4FFD-9DEB-DDD3291947DA");
            _PersonProfilesFunctionality.Setup(mock => mock.CreateAsync(It.IsAny<PersonProfile>())).ReturnsAsync(_bllPerson);
            _PersonProfilesFunctionality.Setup(mock => mock.ReadAsync(It.IsAny<string>())).ReturnsAsync(_bllPerson);
            _PersonProfilesFunctionality.Setup(mock => mock.UpdateAsync(It.IsAny<PersonProfile>())).ReturnsAsync(_bllPerson);
            _PersonProfilesFunctionality.Setup(mock => mock.DeleteAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
        }

        [TestMethod]
        public async Task Create()
        {
            var createdPerson = await _controller.CreateAsync(_bllPerson);
            Assert.IsNotNull(createdPerson);
            Assert.AreEqual(_bllPerson, createdPerson);
        }

        [TestMethod]
        public async Task Read()
        {
            var person = await _controller.ReadAsync(_bllPerson.Id);
            Assert.IsNotNull(person);
            Assert.AreEqual(_bllPerson, person);

        }

        [TestMethod]
        public async Task Update()
        {
            var person = await _controller.UpdateAsync(_bllPerson);
            Assert.IsNotNull(person);
            Assert.AreEqual(_bllPerson, person);

        }

        [TestMethod]
        public async Task Delete()
        {
            await _controller.DeleteAsync(_bllPerson.Id);

        }
    }
}

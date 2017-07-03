using System;
using System.Threading.Tasks;
using Frobozz.PersonProfiles.Bll;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Frobozz.PersonProfiles.FulcrumFacade.WebApi.Controllers;
using Frobozz.PersonProfiles.FulcrumFacade.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi.Tests
{
    [TestClass]
    public class PersonProfilesTests
    {
        private static readonly string Namespace = typeof(PersonProfilesTests).Namespace;
        private IPersonProfilesService _controller;
        private Mock<IPersonProfilesFunctionality> _personProfilesFunctionality;
        private IPersonProfile _bllPerson;
        private IPersonProfile _servicePerson;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
           
            _personProfilesFunctionality = new Mock<IPersonProfilesFunctionality>();
            _controller = new PersonProfilesController(_personProfilesFunctionality.Object);
            var id = Guid.NewGuid().ToString();
            var eTag = Guid.NewGuid().ToString();
            _bllPerson = new PersonProfile
            {
                Id = id,
                ETag = eTag,
                GivenName = "Joe",
                Surname = "Smith"
            };
            _servicePerson = new ServicePersonProfile
            {
                Id = id,
                ETag = eTag,
                GivenName = "Joe",
                Surname = "Smith"
            };
            FulcrumAssert.IsValidated(_bllPerson, $"{Namespace}: C3A453C9-77AA-4FFD-9DEB-DDD3291947DA");
            _personProfilesFunctionality.Setup(mock => mock.CreateAsync(It.IsAny<IPersonProfile>())).ReturnsAsync(_bllPerson);
            _personProfilesFunctionality.Setup(mock => mock.ReadAsync(It.IsAny<string>())).ReturnsAsync(_bllPerson);
            _personProfilesFunctionality.Setup(mock => mock.UpdateAsync(It.IsAny<IPersonProfile>())).ReturnsAsync(_bllPerson);
            _personProfilesFunctionality.Setup(mock => mock.DeleteAsync(It.IsAny<string>())).Returns(Task.FromResult(0));
        }

        [TestMethod]
        public async Task Create()
        {
            var createdPerson = await _controller.CreateAsync(_servicePerson);
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
            var person = await _controller.UpdateAsync(_servicePerson);
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

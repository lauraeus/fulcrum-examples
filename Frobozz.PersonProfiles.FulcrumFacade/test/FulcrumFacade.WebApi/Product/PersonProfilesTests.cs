using System.Threading.Tasks;
using Frobozz.PersonProfiles.Bll;
using Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Frobozz.PersonProfiles.FulcrumFacade.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Frobozz.PersonProfiles.FulcrumFacade.WebApi.Tests.Product
{
    [TestClass]
    public class PersonProfilesTests
    {
        private GeocodesController _controller;
        private Mock<IPersonProfilesFunctionality> _PersonProfilesFunctionality;
        private Location _bllLocation;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
           
            _PersonProfilesFunctionality = new Mock<IPersonProfilesFunctionality>();
            _controller = new GeocodesController(_PersonProfilesFunctionality.Object);
            _bllLocation = new Location
            {
                Latitude = "1.23",
                Longitude = "3.14"
            };
            _PersonProfilesFunctionality.Setup(mock => mock.GeocodeAsync(It.IsAny<Address>())).ReturnsAsync(_bllLocation);
        }

        [TestMethod]
        public async Task Successful()
        {
            var address = new Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
                Country = "Sweden"
            };
            var resultLocation = await _controller.GeocodeAsync(address);
            Assert.IsNotNull(resultLocation);
            Assert.AreEqual(_bllLocation.Latitude, resultLocation.Latitude);
            Assert.AreEqual(_bllLocation.Longitude, resultLocation.Longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(FulcrumServiceContractException))]
        public async Task NoAddressCountry()
        {
            var address = new Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
            };
            var resultLocation = await _controller.GeocodeAsync(address);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupplierCompany.SystemFacade.Sl.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SupplierCompany.SystemFacade.Bll;
using Xlent.Lever.Libraries2.Standard.Error.Logic;
using SM = SupplierCompany.SystemFacade.Fulcrum.Contract;

namespace SupplierCompany.SystemFacade.Sl.WebApi.Tests.Product
{
    [TestClass]
    public class GeocodingTests
    {
        private GeocodesController _controller;
        private Mock<IGeocodingFunctionality> _geocodingFunctionality;
        private SM.Location _bllLocation;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
           
            _geocodingFunctionality = new Mock<IGeocodingFunctionality>();
            _controller = new GeocodesController(_geocodingFunctionality.Object);
            _bllLocation = new SM.Location
            {
                Latitude = "1.23",
                Longitude = "3.14"
            };
            _geocodingFunctionality.Setup(mock => mock.GeocodeAsync(It.IsAny<SM.Address>())).ReturnsAsync(_bllLocation);
        }

        [TestMethod]
        public async Task Successful()
        {
            var address = new SM.Address
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
            var address = new SM.Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
            };
            var resultLocation = await _controller.GeocodeAsync(address);
        }
    }
}

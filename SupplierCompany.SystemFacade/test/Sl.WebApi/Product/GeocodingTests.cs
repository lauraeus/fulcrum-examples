using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupplierCompany.SystemFacade.Sl.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SupplierCompany.SystemFacade.Bll;
using SM = SupplierCompany.SystemFacade.Fulcrum.Contract;

namespace SupplierCompany.SystemFacade.Sl.WebApi.Tests.Product
{
    [TestClass]
    public class GeocodingTests
    {
        private GeocodesController _controller;
        private Mock<IGeocodingFunctionality> _geocodingFunctionality;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
        }

        [TestInitialize]
        public void Initialize()
        {
           
            _geocodingFunctionality = new Mock<IGeocodingFunctionality>();
            _controller = new GeocodesController(_geocodingFunctionality.Object);
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
            var location = new SM.Location
            {
                Latitude = "1.23",
                Longitude = "3.14"
            };
            _geocodingFunctionality.Setup(mock => mock.GeocodeAsync(It.IsAny<SM.Address>())).ReturnsAsync(location);
            var resultLocation = await _controller.GeocodeAsync(address);
            Assert.IsNotNull(resultLocation);
            Assert.AreEqual(location.Latitude, resultLocation.Latitude);
            Assert.AreEqual(location.Longitude, resultLocation.Longitude);
        }
    }
}

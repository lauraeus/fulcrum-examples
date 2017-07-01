using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SupplierCompany.SystemFacade.Dal.WebApi.GoogleGeocode.Clients;
using SupplierCompany.SystemFacade.Fulcrum.Contract.Geocoding;
using Xlent.Lever.Libraries2.Standard.Error.Logic;
using SM = SupplierCompany.SystemFacade.Fulcrum.Contract;
using DM = SupplierCompany.SystemFacade.Dal.WebApi.GoogleGeocode.Models;

namespace SupplierCompany.SystemFacade.Bll.Tests
{
    [TestClass]
    public class GeocodingTest
    {
        private Mock<IGeocodeClient> _geocodeClientMock;
        private IGeocodingFunctionality _geocodeFunctionality;

        [TestInitialize]
        public void Initialize()
        {
            _geocodeClientMock = new Mock<IGeocodeClient>();
            _geocodeFunctionality = new GeocodingFunctionality(_geocodeClientMock.Object);
        }

        [TestMethod]
        public async Task Success()
        {
            _geocodeClientMock.Setup(mock => mock.GeocodeAsync(It.IsAny<DM.Address>())).Returns(Task.FromResult(CreateResponse()));
            var address = new Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
                Country = "Sweden"
            };
            var location = await _geocodeFunctionality.GeocodeAsync(address);
            Assert.IsNotNull(location);
            Assert.IsNotNull(location.Latitude);
            Assert.IsNotNull(location.Longitude);

        }

        [TestMethod]
        [ExpectedException(typeof(FulcrumContractException))]
        public async Task NoAddressCountry()
        {
            var address = new Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
            };
            var resultLocation = await _geocodeFunctionality.GeocodeAsync(address);
        }

        private DM.GeocodingResponse CreateResponse()
        {
            return new DM.GeocodingResponse
            {
                status = "OK",
                results = new []
                {
                    new DM.Result
                    {
                        geometry = new DM.Geometry
                        {
                            location = new DM.Location
                            {
                                lng = "-122.0842499",
                                lat = "37.4224764"
                            }
                        }
                    }
                }
            };
        }
    }
}

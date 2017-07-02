using System.Threading.Tasks;
using Frobozz.FulcrumFacade.Dal.WebApi.GoogleGeocode.Clients;
using Frobozz.FulcrumFacade.Dal.WebApi.GoogleGeocode.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Frobozz.FulcrumFacade.Bll.Tests
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
            _geocodeClientMock.Setup(mock => mock.GeocodeAsync(It.IsAny<Address>())).Returns(Task.FromResult(CreateResponse()));
            var address = new Fulcrum.Contract.Geocoding.Address
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
            var address = new Fulcrum.Contract.Geocoding.Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
            };
            var resultLocation = await _geocodeFunctionality.GeocodeAsync(address);
        }

        private GeocodingResponse CreateResponse()
        {
            return new GeocodingResponse
            {
                status = "OK",
                results = new []
                {
                    new Result
                    {
                        geometry = new Geometry
                        {
                            location = new Location
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

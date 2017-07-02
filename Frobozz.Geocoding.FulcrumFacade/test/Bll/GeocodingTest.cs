using System.Threading.Tasks;
using Frobozz.Geocoding.Dal.WebApi.GoogleGeocoding.Clients;
using DM = Frobozz.Geocoding.Dal.WebApi.GoogleGeocoding.Models;
using SM = Frobozz.Geocoding.FulcrumFacade.Contract.Geocoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Frobozz.Geocoding.Bll.Tests
{
    [TestClass]
    public class GeocodingTest
    {
        private Mock<IGeocodingClient> _GeocodingClientMock;
        private IGeocodingFunctionality _geocodingFunctionality;

        [TestInitialize]
        public void Initialize()
        {
            _GeocodingClientMock = new Mock<IGeocodingClient>();
            _geocodingFunctionality = new GeocodingFunctionality(_GeocodingClientMock.Object);
        }

        [TestMethod]
        public async Task Success()
        {
            _GeocodingClientMock.Setup(mock => mock.GeocodeAsync(It.IsAny<DM.Address>())).ReturnsAsync(CreateResponseOk());
            var address = new SM.Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
                Country = "Sweden"
            };
            var location = await _geocodingFunctionality.GeocodeAsync(address);
            Assert.IsNotNull(location);
            Assert.IsNotNull(location.Latitude);
            Assert.IsNotNull(location.Longitude);

        }

        [TestMethod]
        [ExpectedException(typeof(FulcrumContractException))]
        public async Task NoAddressCountry()
        {
            var address = new SM.Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
            };
            var resultLocation = await _geocodingFunctionality.GeocodeAsync(address);
        }

        private DM.GeocodingResponse CreateResponseOk()
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

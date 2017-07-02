using System.Linq;
using System.Threading.Tasks;
using Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Clients;
using Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.PersonProfiles.Dal.WebApi.Test
{
    [TestClass]
    public class PersonProfilesTest
    {
        private IPersonProfilesClient _client;
        private Mock<IRestClient> _restClientMock;
        private Location _mockLocation;
        private string _mockMessage;

        [TestInitialize]
        public void Initialize()
        {
            _mockMessage = "Random error message 6EE73B6E-45FA-4394-9B42-076669470C52";
            _mockLocation = new Location
            {
                lng = "-123.456",
                lat = "33.33333"
            };
            _restClientMock = new Mock<IRestClient>();
            _client = new PersonProfilesClient(_restClientMock.Object);
        }

        [TestMethod]
        public async Task Successful()
        {
            _restClientMock.Setup(mock => mock.GetAsync<PersonProfilesResponse>(It.IsAny<string>())).ReturnsAsync(CreateResponseOk(_mockLocation));
            var address = new Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
                Country = "Sweden"
            };
            var response = await _client.GeocodeAsync(address);
            Assert.IsNotNull(response?.results);
            Assert.AreEqual("OK", response.status);
            var result = response?.results?.FirstOrDefault();
            var location = result?.geometry?.location;
            Assert.IsNotNull(location);
            Assert.AreEqual(_mockLocation.lng, location.lng);
            Assert.AreEqual(_mockLocation.lat, location.lat);
        }

        [TestMethod]
        public async Task ZeroResult()
        {
            _restClientMock.Setup(mock => mock.GetAsync<PersonProfilesResponse>(It.IsAny<string>())).ReturnsAsync(CreateResponseZeroResults(_mockMessage));
            var address = new Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
                Country = "Sweden"
            };
            var response = await _client.GeocodeAsync(address);
            Assert.IsNotNull(response);
            Assert.AreEqual("ZERO_RESULTS", response.status);
            Assert.IsNotNull(response.error_message);
            Assert.IsTrue(response.error_message.Contains(_mockMessage));
        }

        private static PersonProfilesResponse CreateResponseOk(Location location)
        {
            return new PersonProfilesResponse
            {
                status = "OK",
                results = new[]
                {
                    new Result
                    {
                        geometry = new Geometry
                        {
                            location = location
                        }
                    }
                }
            };
        }

        private static PersonProfilesResponse CreateResponseZeroResults(string errorMessage = null)
        {
            return new PersonProfilesResponse
            {
                status = "ZERO_RESULTS",
                error_message = errorMessage
            };
        }
    }
}

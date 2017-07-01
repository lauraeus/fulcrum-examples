using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Clients;
using SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Models;

namespace SystemFacade.Dal.RestClients.Test
{
    [TestClass]
    public class GeocodingTest
    {
        private IGeocodeClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new GeocodeClient();
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
            var response = await _client.GeocodeAsync(address);
            Assert.IsNotNull(response?.results);
            Assert.AreEqual("OK", response.status);
            var result = response?.results?.FirstOrDefault();
            var location = result?.geometry?.location;
            Assert.IsNotNull(location.lat);
            Assert.IsNotNull(location.lng);
        }
    }
}

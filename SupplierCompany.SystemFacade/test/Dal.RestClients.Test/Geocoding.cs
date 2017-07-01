using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Clients;
using SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Models;

namespace SystemFacade.Dal.RestClients.Test
{
    [TestClass]
    public class Geocoding

    {
        private IGeocodeClient _client;

        [TestInitialize]
        public void Initialize()
        {
            _client = new GeocodeClient();
        }

        [TestMethod]
        public void Successful()
        {
            var address = new Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156"
            };
            var location = _client.GeocodeAsync(address);
            Assert.IsNotNull(location);
        }
    }
}

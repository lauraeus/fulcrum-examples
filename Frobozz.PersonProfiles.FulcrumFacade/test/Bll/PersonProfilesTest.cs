using System.Threading.Tasks;
using Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Clients;
using DM = Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Models;
using SM = Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace Frobozz.PersonProfiles.Bll.Tests
{
    [TestClass]
    public class PersonProfilesTest
    {
        private Mock<IPersonProfilesClient> _PersonProfilesClientMock;
        private IPersonProfilesFunctionality _PersonProfilesFunctionality;

        [TestInitialize]
        public void Initialize()
        {
            _PersonProfilesClientMock = new Mock<IPersonProfilesClient>();
            _PersonProfilesFunctionality = new PersonProfilesFunctionality(_PersonProfilesClientMock.Object);
        }

        [TestMethod]
        public async Task Success()
        {
            _PersonProfilesClientMock.Setup(mock => mock.GeocodeAsync(It.IsAny<DM.Address>())).ReturnsAsync(CreateResponseOk());
            var address = new SM.Address
            {
                Row1 = "Regeringsgatan 67",
                PostCode = "11156",
                Country = "Sweden"
            };
            var location = await _PersonProfilesFunctionality.GeocodeAsync(address);
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
            var resultLocation = await _PersonProfilesFunctionality.GeocodeAsync(address);
        }

        private DM.PersonProfilesResponse CreateResponseOk()
        {
            return new DM.PersonProfilesResponse
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

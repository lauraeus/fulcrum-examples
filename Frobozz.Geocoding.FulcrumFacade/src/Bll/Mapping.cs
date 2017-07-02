using Frobozz.FulcrumFacade.Dal.WebApi.GoogleGeocode.Models;
using Location = Frobozz.FulcrumFacade.Fulcrum.Contract.Geocoding.Location;

namespace Frobozz.FulcrumFacade.Bll
{
    public static class Mapping
    {
        public static Address ToStorage(Fulcrum.Contract.Geocoding.Address source)
        {
            if (source == null) return null;
            var target = new Address
            {
                Row1 = source.Row1,
                Row2 = source.Row2,
                PostCode = source.PostCode,
                PostTown = source.PostTown,
                Country = source.Country
            };
            return target;
        }
        public static Location ToService(Dal.WebApi.GoogleGeocode.Models.Location source)
        {
            if (source == null) return null;
            var target = new Location
            {
                Longitude = source.lng,
                Latitude = source.lat
            };
            return target;
        }
    }
}

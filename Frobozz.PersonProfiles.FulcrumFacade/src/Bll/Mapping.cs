using DM = Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Models;
using SM = Frobozz.PersonProfiles.FulcrumFacade.Contract.PersonProfiles;

namespace Frobozz.PersonProfiles.Bll
{
    public static class Mapping
    {
        public static DM.Address ToStorage(SM.Address source)
        {
            if (source == null) return null;
            var target = new DM.Address
            {
                Row1 = source.Row1,
                Row2 = source.Row2,
                PostCode = source.PostCode,
                PostTown = source.PostTown,
                Country = source.Country
            };
            return target;
        }
        public static SM.Location ToService(DM.Location source)
        {
            if (source == null) return null;
            var target = new SM.Location
            {
                Longitude = source.lng,
                Latitude = source.lat
            };
            return target;
        }
    }
}

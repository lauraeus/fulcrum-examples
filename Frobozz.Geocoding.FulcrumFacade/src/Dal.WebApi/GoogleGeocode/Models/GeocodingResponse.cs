// ReSharper disable All
#pragma warning disable IDE1006 // Naming Styles
namespace Frobozz.FulcrumFacade.Dal.WebApi.GoogleGeocode.Models
{
    /// <summary>
    /// A response from a geocode request, as specified at https://developers.google.com/maps/documentation/geocoding/intro#GeocodingResponses
    /// </summary>
    public class GeocodingResponse
    {
        /// <summary>
        /// See https://developers.google.com/maps/documentation/geocoding/intro#StatusCodes
        /// </summary>
        public string status { get; set; }
        public string error_message { get; set; }

        public Result[] results { get; set; }
    }

    public class Result
    {
        public AddressComponent[] address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public string[] types { get; set; }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public string[] types { get; set; }
    }

    public class Geometry
    {
        public Location location;
        public string location_type;
        public ViewPort viewport;
    }

    public class Location
    {
        public string lat;
        public string lng;
    }

    public class ViewPort
    {
        public Location northeast;
        public Location southwest;
    }
}
#pragma warning restore IDE1006 // Naming Styles
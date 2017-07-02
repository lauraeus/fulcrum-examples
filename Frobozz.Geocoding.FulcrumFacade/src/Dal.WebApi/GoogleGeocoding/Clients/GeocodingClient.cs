using System;
using System.Threading.Tasks;
using Frobozz.Geocoding.Dal.WebApi.GoogleGeocoding.Models;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.Geocoding.Dal.WebApi.GoogleGeocoding.Clients
{
    /// <summary>
    /// A client for accessing the Google Geocoding API; https://developers.google.com/maps/documentation/geocoding/intro
    /// </summary>
    public class GeocodingClient : IGeocodingClient
    {
        private readonly IRestClient _restClient;

        public GeocodingClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<GeocodingResponse> GeocodeAsync(Address address)
        {
            InternalContract.RequireNotNull(address, nameof(address));
            InternalContract.RequireValidated(address, nameof(address));
            var relativeUrl = $"/json?address={ToUrlParameter(address)}";
            return await _restClient.GetAsync<GeocodingResponse>(relativeUrl);
        }

        private string ToUrlParameter(Address address)
        {
            InternalContract.RequireNotNull(address, nameof(address));
            InternalContract.RequireValidated(address, nameof(address));
            var oneLine = $"{address.Row1}, {address.Row2}, {address.PostCode} {address.PostTown}, {address.Country}";
            return Uri.EscapeDataString(oneLine);
        }
    }
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Clients;
using SM = SupplierCompany.SystemFacade.Fulcrum.Contract;
using DM = SupplierCompany.SystemFacade.Dal.RestClients.GoogleGeocode.Models;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Error.Logic;

namespace SupplierCompany.SystemFacade.Bll
{
    public class GeocodingFunctionality : IGeocodingFunctionality
    {
        private static readonly string Namespace = typeof(GeocodingFunctionality).Namespace;
        private readonly IGeocodeClient _client;

        public GeocodingFunctionality(IGeocodeClient client)
        {
            _client = client;
        }
        public async Task<SM.Location> GeocodeAsync(SM.Address address, bool mustBeUnique)
        {
            InternalContract.RequireNotNull(address, nameof(address));
            InternalContract.RequireValidated(address, nameof(address));

            var response = await _client.GeocodeAsync(Mapping.ToStorage(address));
            FulcrumAssert.IsNotNull(response, $"{Namespace}: 9410F4F3-FCFB-4B82-9E9D-5061F41EB31B");
            switch (response.status)
            {
                case "OK":
                    FulcrumAssert.AreEqual(1, response.results.Length, $"{Namespace}: D7A969EE-B513-4093-ACB1-4D3B8168B4A6");
                    var firstResult = response.results?.FirstOrDefault();
                    FulcrumAssert.IsNotNull(firstResult?.geometry?.location, $"{Namespace}: D7A969EE-B513-4093-ACB1-4D3B8168B4A6", $"Google geocoding returned an unexpected result: {JObject.FromObject(response).ToString(Formatting.Indented)}");
                    if (mustBeUnique && response.results.Length > 1) throw new FulcrumContractException($"The geoconding was not unique. There were ({response.results.Length}) results in the response.");
                    var location = Mapping.ToService(firstResult?.geometry?.location);
                    FulcrumAssert.IsNotNull(location, $"{Namespace}: F21B761B-0CD4-4063-8780-70F2921301D1");
                    FulcrumAssert.IsValidated(location, $"{Namespace}: 5DB577C2-C3EC-4C0A-AFF1-F018635CF8AF");
                    return location;
                case "ZERO_RESULTS":
                    throw new FulcrumNotFoundException(response.error_message ?? $"Geocoding of Address {address} returned no results.");
                case "OVER_QUERY_LIMIT":
                    throw new FulcrumForbiddenAccessException(response.error_message ?? "Google geocoding returned a status that says that the current API key is over the query limit.");
                case "REQUEST_DENIED":
                    throw new FulcrumForbiddenAccessException(response.error_message ?? "Google geocoding returned a status that says that the request was denied. Invalid API key?");
                case "INVALID_REQUEST":
                    FulcrumAssert.Fail($"{Namespace}: D2268351-8B84-4CCE-B5AB-793A9C4C0366", response.error_message ?? "Google geocoding returned a status that generally indicates that the query (address, components or latlng) is missing");
                    throw new ApplicationException("Assertion failed to stop execution.");
                case "UNKNOWN_ERROR":
                    throw new FulcrumTryAgainException(response.error_message ?? "Google geocoding returned a status that indicates that the request could not be processed due to a server error. The request may succeed if you try again.");
                default:
                    FulcrumAssert.Fail($"{Namespace}: 96D375F3-2D5A-42B3-9168-145AD0D68CA7", response.error_message ?? $"Google geocoding returned a an unkown status code ({response.status})");
                    throw new ApplicationException("Assertion failed to stop execution.");
            }
        }

        public async Task<SM.Location> GeocodeAsync(SM.Address address)
        {
            return await GeocodeAsync(address, false);
        }
    }
}
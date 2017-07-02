using System;
using System.Threading.Tasks;
using Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Models;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.WebApi.RestClientHelper;

namespace Frobozz.PersonProfiles.Dal.WebApi.GooglePersonProfiles.Clients
{
    /// <summary>
    /// A client for accessing the Google PersonProfiles API; https://developers.google.com/maps/documentation/PersonProfiles/intro
    /// </summary>
    public class PersonProfilesClient : IPersonProfilesClient
    {
        private readonly IRestClient _restClient;

        public PersonProfilesClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<PersonProfilesResponse> GeocodeAsync(Address address)
        {
            InternalContract.RequireNotNull(address, nameof(address));
            InternalContract.RequireValidated(address, nameof(address));
            var relativeUrl = $"/json?address={ToUrlParameter(address)}";
            return await _restClient.GetAsync<PersonProfilesResponse>(relativeUrl);
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

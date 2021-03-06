﻿using System.Threading.Tasks;
using System.Web.Http;
using Frobozz.Geocoding.Bll;
using Frobozz.Geocoding.FulcrumFacade.Contract.Geocoding;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Frobozz.Geocoding.FulcrumFacade.WebApi.Controllers
{
    /// <summary>
    /// ApiController for Product that does inputcontrol. Logic is separated into another layer. 
    /// </summary>
    [RoutePrefix("api/v1/Geocodes")]
    public class GeocodesController : ApiController, IGeocodingService
    {
        private static readonly string Namespace = typeof(GeocodesController).Namespace;
        /// <summary>
        /// The actual implementation
        /// </summary>
        public IGeocodingFunctionality GeocodingFunctionality { get; }

        /// <summary>
        /// Constructor that takes a logic layer for product. 
        /// </summary>
        /// <param name="geocodingFunctionality">Dependency injected logic layer</param>
        public GeocodesController(IGeocodingFunctionality geocodingFunctionality)
        {
            GeocodingFunctionality = geocodingFunctionality;
        }

        /// <summary>
        /// Returnes the first found Location that matches the <paramref name="address"/>.
        /// </summary>
        /// <param name="address">The address to look for</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("")]
        public async Task<Location> GeocodeAsync(Address address)
        {
            ServiceContract.RequireNotNull(address, nameof(address));
            ServiceContract.RequireValidated(address, nameof(address));
            var location = await GeocodingFunctionality.GeocodeAsync(address);
            FulcrumAssert.IsNotNull(location, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            FulcrumAssert.IsValidated(location, $"{Namespace}: 56B82634-89A6-4EE4-969D-B7FFB4F9C016");
            return location;
        }
    }
}

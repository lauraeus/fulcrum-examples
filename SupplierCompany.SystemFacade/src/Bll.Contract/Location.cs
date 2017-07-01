﻿using Xlent.Lever.Libraries2.Standard.Assert;

namespace SupplierCompany.SystemFacade.Fulcrum.Contract
{
    /// <summary>
    /// A latitude + longitude location
    /// </summary>
    public class Location : IValidatable
    {
        /// <summary>
        /// The longitude
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// The latitude
        /// </summary>
        public string Latitude { get; set; }

        /// <inheritdoc />
        public void Validate(string errorLocation, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(Longitude, nameof(Longitude), errorLocation);
            FulcrumValidate.IsNotNullOrWhiteSpace(Latitude, nameof(Latitude), errorLocation);
        }
    }
}

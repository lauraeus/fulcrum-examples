using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Xlent.Lever.CapabilityTemplate.Bll.Contract.Inbound.Product;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Xlent.Lever.CapabilityTemplate.Sl.WebApi.Model
{
    /// <summary>
    /// A product that can be sold
    /// </summary>
    public class Product : IStorableItemRecommended<string>
    {
        /// <inheritdoc/>
        [RegularExpression("[0-9]+")]
        public string Id { get; set; }

        /// <inheritdoc/>
        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Name { get; set; }

        /// <inheritdoc/>
        public string ETag { get; set; }

        /// <inheritdoc cref="IProduct.Category"/>
        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Category { get; set; }

        /// <inheritdoc cref="IProduct.Price"/>
        [Required]
        [Range(0, double.PositiveInfinity)]
        public double Price { get; set; }

        /// <inheritdoc cref="IProduct.DateAdded"/>
        [Required]
        public DateTimeOffset DateAdded { get; set; }

        /// <inheritdoc/>
        public void Validate(string errorLocaction, string propertyPath = "")
        {
            FulcrumValidate.IsNotNullOrWhiteSpace(Id, nameof(Id), errorLocaction);
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocaction);
            FulcrumValidate.MatchesRegExp("^[a-zA-Z]+$", Name, nameof(Name), errorLocaction);
            FulcrumValidate.IsNotNullOrWhiteSpace(Category, nameof(Category), errorLocaction);
            FulcrumValidate.MatchesRegExp("^[a-zA-Z]+$", Category, nameof(Category), errorLocaction);
            FulcrumValidate.IsGreaterThanOrEqualTo(0.0, Price, nameof(Price), errorLocaction);
            FulcrumValidate.IsNotDefaultValue(DateAdded, nameof(DateAdded), errorLocaction);
            FulcrumValidate.IsLessThanOrEqualTo(DateTimeOffset.Now, DateAdded, nameof(DateAdded), errorLocaction);
        }
    }
}
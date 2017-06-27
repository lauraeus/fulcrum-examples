using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Acme.FulcrumFacade.Sl.WebApi.Model
{
    public class Product : IValidatable
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Category { get; set; }
      
        [Required]
        [Range(0, double.PositiveInfinity)]
        public double Price { get; set; }
        
        [Required]
        public DateTimeOffset DateAdded { get; set; }

        /// <inheritdoc/>
        public void Validate(string errorLocaction, string propertyPath = "")
        {
            FulcrumValidate.IsGreaterThan(0, Id, nameof(Id), errorLocaction);
            FulcrumValidate.IsNotNullOrWhiteSpace(Name, nameof(Name), errorLocaction);
            FulcrumValidate.IsTrue(Regex.IsMatch(Name, "^[a-zA-Z]+$"), errorLocaction, $"Property {nameof(Name)} must only consist of upper or lower case a-z.");
            FulcrumValidate.IsNotNullOrWhiteSpace(Category, nameof(Category), errorLocaction);
            FulcrumValidate.IsTrue(Regex.IsMatch(Category, "^[a-zA-Z]+$"), errorLocaction, $"Property {nameof(Category)} must only consist of upper or lower case a-z.");
            FulcrumValidate.IsGreaterThanOrEqualTo(0.0, Price, nameof(Price), errorLocaction);
            FulcrumValidate.IsNotDefaultValue(DateAdded, nameof(DateAdded), errorLocaction);
            FulcrumValidate.IsLessThanOrEqualTo(DateTimeOffset.Now, DateAdded, nameof(DateAdded), errorLocaction);
        }
    }
}
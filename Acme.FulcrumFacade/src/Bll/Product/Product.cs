using System;
using System.Text.RegularExpressions;
using Acme.FulcrumFacade.Bll.Contract.Product;
using Xlent.Lever.Libraries2.Standard.Assert;

namespace Acme.FulcrumFacade.Bll.Product
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
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
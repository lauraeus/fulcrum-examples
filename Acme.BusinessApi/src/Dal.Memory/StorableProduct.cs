using System;
using System.Text.RegularExpressions;
using Acme.FulcrumFacade.Dal.Contract.Product;
using Xlent.Lever.Libraries2.Standard.Assert;
using Xlent.Lever.Libraries2.Standard.Storage.Model;

namespace Acme.FulcrumFacade.Dal.Memory
{
    public class StorableProduct : StorableItem<int>, IStorableProduct
    {
        public string Category { get; set; }
        public double Price { get; set; }
        public DateTimeOffset DateAdded { get; set; }

        /// <inheritdoc/>
        public override void Validate(string errorLocaction, string propertyPath = "")
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